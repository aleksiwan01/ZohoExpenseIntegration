using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static PX.Objects.EP.EPDelegationOf;
using static PX.Objects.TX.CSTaxCalcType;

namespace Webhooks
{
    public class ZohoAPICalls
    {
        private JsonSerializer Serialiser = new JsonSerializer();

        public string RefreshZohoExpenseToken()
        {
            ZohoSetupMaint zohoGraph = PXGraph.CreateInstance<ZohoSetupMaint>();
            ZohoSetup setup = SelectFrom<ZohoSetup>.View.Select(zohoGraph);

            string refreshToken = setup.RefreshToken;
            string clientId = setup.ClientID;
            string clientSecret = setup.ClientSecret;
            string redirect_uri = setup.RedirectURI;
            string grant_type = "refresh_token";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.zoho.eu/oauth/v2/token?refresh_token="
                + refreshToken + "&client_id=" + clientId + "&client_secret=" + clientSecret
                + "&redirect_uri=" + redirect_uri +"&grant_type=" + grant_type);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var jr = new JsonTextReader(new StreamReader(response.GetResponseStream())))
                {
                    var payload = Serialiser.Deserialize<Dictionary<String, object>>(jr);
                    if (payload is null)
                    {
                        // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                        throw new PXException("Failed to refresh Zoho token.");
                    }
                    var t = payload;

                    string accessToken = payload["access_token"]?.ToString();

                    return accessToken;
                }
            }

        }

        public List<ZohoExpenseData> GetExpenses(string orgId, string token, string start_date, string end_date, string reportID)
        {
            token = RefreshZohoExpenseToken();
            List<ZohoExpenseData> expenses = new List<ZohoExpenseData>();

            if (reportID == null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zohoapis.eu/expense/v1/expenses?date_start="
                + start_date + "&end_date=" + end_date);
                request.Headers.Add("X-com-zoho-expense-organizationid", orgId);
                request.Headers.Add("Authorization", $"Zoho-oauthtoken {token}");
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var jr = new JsonTextReader(new StreamReader(response.GetResponseStream())))
                    {
                        var payload = Serialiser.Deserialize<Dictionary<String, object>>(jr);

                        JArray jExpenses = payload["expenses"] as JArray;
                        foreach (var jExpense in jExpenses)
                        {
                            var dateString = jExpense["date"].ToString();

                            JArray documents = (JArray)jExpense["documents"];
                            
                            DateTime.TryParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date);

                            ZohoExpenseData expData = new ZohoExpenseData()
                            {
                                expense_id = jExpense["expense_id"].ToString(),
                                report_name = jExpense["report_name"].ToString(),
                                date = date,
                                category_name = jExpense["category_name"].ToString(),
                                amount = Convert.ToDecimal(jExpense["amount"]),
                                payment_mode = jExpense["payment_mode"].ToString(),
                                policy_name = jExpense["policy_name"].ToString(),
                                duplicate_status = jExpense["duplicate_status"].ToString(),
                                docs = new List<ZohoDocuments>()
                            };
                            
                            foreach(var document in documents)
                            {
                                ZohoDocuments doc = new ZohoDocuments
                                {
                                    docId = document["document_id"].ToString(),
                                    fileName = document["file_name"].ToString()
                                };
                                expData.docs.Add(doc);
                            }
                            expenses.Add(expData);
                        }
                    }
                }
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.zohoapis.eu/expense/v1/expensereports/{reportID}");
                request.Headers.Add("X-com-zoho-expense-organizationid", orgId);
                request.Headers.Add("Authorization", $"Zoho-oauthtoken {token}");
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var jr = new JsonTextReader(new StreamReader(response.GetResponseStream())))
                    {
                        var payload = Serialiser.Deserialize<Dictionary<String, object>>(jr);

                        PXTrace.WriteInformation($"{payload.ToString()}");
                        JObject jReport = payload["expense_report"] as JObject;
                        JArray jExpenses = jReport["expenses"] as JArray;
                        foreach (var jExpense in jExpenses)
                        {
                            var dateString = jExpense["date"].ToString();

                            DateTime.TryParseExact(dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date);

                            JArray documents = (JArray)jExpense["documents"];

                            ZohoExpenseData expData = new ZohoExpenseData
                            {
                                expense_id = jExpense["expense_id"].ToString() ?? "",
                                report_name = jExpense["report_name"].ToString() ?? "",
                                date = date,
                                category_name = jExpense["category_name"].ToString() ?? "",
                                amount = Convert.ToDecimal(jExpense["amount"]),
                                payment_mode = jExpense["payment_mode"].ToString() ?? "",
                                policy_name = jExpense["policy_name"].ToString() ?? "",
                                duplicate_status = jExpense["duplicate_status"].ToString() ?? "",
                                docs = new List<ZohoDocuments>()
                            };

                            foreach (var document in documents)
                            {
                                ZohoDocuments doc = new ZohoDocuments
                                {
                                    docId = document["document_id"].ToString(),
                                    fileName = document["file_name"].ToString()
                                };
                                expData.docs.Add(doc);
                            }

                            expenses.Add(expData);
                        }
                    }
                }
            }
            
            return expenses;
        }

        public List<ZohoReport> GetExpenseReport(string orgId, string token)
        {
            token = RefreshZohoExpenseToken();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zohoapis.eu/expense/v1/expensereports");
            request.Headers.Add("X-com-zoho-expense-organizationid", orgId);
            request.Headers.Add("Authorization", $"Zoho-oauthtoken {token}");
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var jr = new JsonTextReader(new StreamReader(response.GetResponseStream())))
                {
                    var payload = Serialiser.Deserialize<Dictionary<String, object>>(jr);

                    List<ZohoReport> reports = new List<ZohoReport>();

                    JArray jReports = payload["expense_reports"] as JArray;
                    foreach (var jReport in jReports)
                    {
                        reports.Add(new ZohoReport()
                        {
                            ReportID = jReport["report_id"].ToString(),
                            ReportName = jReport["report_name"].ToString(),
                            Name = jReport["submitter_name"].ToString(),
                            ReportDate = jReport["submitted_date"].ToString(),
                            StartDate = jReport["start_date"].ToString(),
                            EndDate = jReport["end_date"].ToString(),
                            Status = jReport["status"].ToString(),  
                            SubmitterEmail = jReport["submitter_email"].ToString(),
                            Amount = Convert.ToDecimal(jReport["total"]),
                            ReimbursableAmount = Convert.ToDecimal(jReport["reimbursable_total"]),
                            NonReimbursableAmount = Convert.ToDecimal(jReport["non_reimbursable_total"])
                        });
                    }

                    return reports;
                }
            }
        }

        public void AttachReceipts(string expenseId, string docId, string token, string orgId, string fileName)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.zohoapis.eu/expense/v1/expenses/" + expenseId + "/documents/" + docId);
                request.Headers.Add("X-com-zoho-expense-organizationid", orgId);
                request.Headers.Add("Authorization", $"Zoho-oauthtoken {token}");
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            UploadFileMaintenance fileGraph = PXGraph.CreateInstance<UploadFileMaintenance>();
                            var fileBytes = default(byte[]);
                            using (var memStream = new MemoryStream())
                            {
                                reader.BaseStream.CopyTo(memStream);
                                fileBytes = memStream.ToArray();
                            }

                            PX.SM.FileInfo fileInfo = new PX.SM.FileInfo(fileName, null, fileBytes);
                            fileGraph.SaveFile(fileInfo, FileExistsAction.CreateVersion);

                            ZohoExpenseEntry zohoGraph = PXGraph.CreateInstance<ZohoExpenseEntry>();

                            ZohoExpense expense = zohoGraph.Header.Current = zohoGraph.Header.Search<ZohoExpense.zohoExpenseID>(expenseId);

                            PXNoteAttribute.AttachFile(
                                zohoGraph.Caches[typeof(ZohoExpense)],
                                expense,
                                fileInfo);

                            fileGraph.Actions.PressSave();
                            zohoGraph.Actions.PressSave();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Acuminator disable once PX1050 HardcodedStringInLocalizationMethod [Justification]
                // Acuminator disable once PX1051 NonLocalizableString [Justification]
                throw new PXException(ex.Message);
            }
        }
    }
}
