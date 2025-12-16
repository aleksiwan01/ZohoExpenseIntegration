using Microsoft.AspNetCore.Http;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    public class ZohoExpenseProcessing : PXGraph<ZohoExpenseProcessing>
    {
        #region Selects
        public PXFilter<ZohoProcess> Process;
        public PXCancel<ZohoProcess> Cancel;
        public SelectFrom<ZohoExpense>.ProcessingView Expenses;
        public SelectFrom<ZohoExpenseReport>.ProcessingView Report;
        #endregion

        #region Actions
        public PXAction<ZohoProcess> ProcessExpensesAction;
        [PXProcessButton(DisplayOnMainToolbar = true)]
        [PXUIField(DisplayName = "Process Expenses")]
        protected virtual IEnumerable processExpensesAction(PXAdapter adapter)
        {
            
            PXLongOperation.StartOperation(this, delegate ()
            {
                ProcessExpenseMethod();
            });
            
            return adapter.Get();
        }

        public PXAction<ZohoProcess> GetExpenseReportsAction;
        [PXProcessButton(DisplayOnMainToolbar = true)]
        [PXUIField(DisplayName = "Get Expense Reports")]
        protected virtual IEnumerable getExpenseReportsAction(PXAdapter adapter)
        {
            PXLongOperation.StartOperation(this, delegate ()
            {
                GetExpenseReportsMethod();
            });
            return adapter.Get();
        }
        #endregion

        #region Methods
        private static void ProcessExpenseMethod()
        {
            using (PXTransactionScope ts = new PXTransactionScope())
            {
                ZohoAPICalls api = new ZohoAPICalls();
                ZohoSetupMaint zohoGraph = PXGraph.CreateInstance<ZohoSetupMaint>();
                ZohoSetup setup = SelectFrom<ZohoSetup>.View.Select(zohoGraph);
                ZohoExpenseProcessing zohoExpenseProcessing = PXGraph.CreateInstance<ZohoExpenseProcessing>();
                string orgId = setup.OrganizationID;
                string token = api.RefreshZohoExpenseToken();
                string startdate = zohoExpenseProcessing.Process.Current.StartDate?.ToString("yyyy-MM-dd");
                string enddate = zohoExpenseProcessing.Process.Current.EndDate?.ToString("yyyy-MM-dd");
                List<ZohoExpenseData> expenses = api.GetExpenses(orgId, token, startdate, enddate, null);
                ZohoExpenseEntry expenseGraph = PXGraph.CreateInstance<ZohoExpenseEntry>();
                foreach (ZohoExpenseData exp in expenses)
                {
                    ZohoExpense header = expenseGraph.Header.Search<ZohoExpense.zohoExpenseID>(exp.expense_id);
                    if (header == null)
                    {
                        header = expenseGraph.Header.Insert(new ZohoExpense()
                        {
                            ZohoExpenseID = exp.expense_id,
                            ReportName = exp.report_name,
                            Date = exp.date,
                            CategoryName = exp.category_name,
                            Description = exp.description,
                            Amount = exp.amount,
                            DuplicateStatus = exp.duplicate_status == "not_duplicate" ? false : true,
                            PaymentMode = exp.payment_mode,
                        });

                        expenseGraph.Header.Update(header);
                        expenseGraph.Actions.PressSave();
                        if(exp.docs is null || exp.docs.Count == 0)
                        {
                            continue;
                        }
                        foreach (ZohoDocuments doc in exp.docs)
                        {
                            api.AttachReceipts(exp.expense_id, doc.docId, token, orgId, doc.fileName);
                        }
                        
                    }
                    else
                    {
                        PXTrace.WriteInformation(Constants.DuplicateExpenses + exp.expense_id + Constants.DuplicateExpensePt2);
                    }
                }

                ts.Complete();
            }
        }

        private static void GetExpenseReportsMethod()
        {
            using (var ts = new PXTransactionScope())
            {
                ZohoAPICalls api = new ZohoAPICalls();
                ZohoSetupMaint zohoGraph = PXGraph.CreateInstance<ZohoSetupMaint>();
                ZohoSetup setup = SelectFrom<ZohoSetup>.View.Select(zohoGraph);
                string orgId = setup.OrganizationID;
                string token = api.RefreshZohoExpenseToken();
                List<ZohoReport> reports = api.GetExpenseReport(orgId, token);
                ZohoExpenseReportEntry reportEntry = PXGraph.CreateInstance<ZohoExpenseReportEntry>();
                foreach (var jReport in reports)
                {
                    ZohoExpenseReport report = reportEntry.Report.Search<ZohoExpenseReport.reportName>(jReport.ReportName);
                    if(report == null)
                    {
                        DateTime.TryParseExact(jReport.ReportDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime reportDate);
                        DateTime.TryParseExact(jReport.StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime startDate);
                        DateTime.TryParseExact(jReport.EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime endDate);

                        report = reportEntry.Report.Current = reportEntry.Report.Insert(new ZohoExpenseReport()
                        {
                            ReportName = jReport.ReportName,
                            Name = jReport.Name,
                            ReportDate = reportDate,
                            StartDate = startDate,
                            EndDate = endDate,
                            Status = jReport.Status,
                            Amount = jReport.Amount,
                            SubmitterEmail = jReport.SubmitterEmail,
                            ReimbursableAmount = jReport.ReimbursableAmount,
                            NonReimbursableAmount = jReport.NonReimbursableAmount,
                        });

                        reportEntry.Save.Press();
                        List<ZohoExpenseData> expenses = api.GetExpenses(orgId, token, null, null, jReport.ReportID);
                        ZohoExpenseEntry expenseGraph = PXGraph.CreateInstance<ZohoExpenseEntry>();
                        foreach (ZohoExpenseData exp in expenses)
                        {
                            ZohoExpense header = expenseGraph.Header.Search<ZohoExpense.expenseID>(exp.expense_id);
                            if (header == null)
                            {
                                header = expenseGraph.Header.Insert(new ZohoExpense()
                                {
                                    ZohoExpenseID = exp.expense_id,
                                    ReportName = exp.report_name,
                                    Date = exp.date,
                                    CategoryName = exp.category_name,
                                    Description = exp.description,
                                    Amount = exp.amount,
                                    DuplicateStatus = exp.duplicate_status == "not_duplicate" ? false : true,
                                    PaymentMode = exp.payment_mode,
                                });
                                expenseGraph.Header.Update(header);
                                expenseGraph.Actions.PressSave();
                            }
                            else
                            {
                                PXTrace.WriteInformation(Constants.DuplicateExpenses + exp.expense_id + Constants.DuplicateExpensePt2);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }

    public class  ZohoProcess : PXBqlTable, IBqlTable
    {
        #region StartDate
        public abstract class startDate : PX.Data.BQL.BqlDateTime.Field<startDate> { }
        [PXDBDate]
        [PXUIField(DisplayName = "Start Date")]
        public virtual DateTime? StartDate { get; set; }
        #endregion

        #region EndDate
        public abstract class endDate : PX.Data.BQL.BqlDateTime.Field<endDate> { }
        [PXDBDate]
        [PXUIField(DisplayName = "End Date")]
        public virtual DateTime? EndDate { get; set; }
        #endregion
    }
}
