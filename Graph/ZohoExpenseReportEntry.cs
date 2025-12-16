using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    public class ZohoExpenseReportEntry : PXGraph<ZohoExpenseReportEntry, ZohoExpenseReport>
    {
        #region Selects
        public SelectFrom<ZohoExpenseReport>.View Report;

        public SelectFrom<ZohoExpense>.
            Where<ZohoExpense.reportName.IsEqual<ZohoExpenseReport.reportName.FromCurrent>>.View Expenses;
        #endregion
    }
}
