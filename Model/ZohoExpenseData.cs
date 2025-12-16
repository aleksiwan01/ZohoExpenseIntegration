using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    [Serializable]
    public class ZohoExpenseData
    {
        public string expense_id { get; set; }
        public string report_name { get; set; }
        public DateTime? date { get; set; }
        public string category_name { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public string payment_mode { get; set; }
        public string policy_name { get; set; }
        public string duplicate_status { get; set; }

        public List<ZohoDocuments> docs { get; set; }
    }

    [Serializable]
    public class ZohoDocuments
    {
        public string docId { get; set; }
        public string fileName { get; set; }
    }

    [Serializable]
    public class ZohoReport
    {
        public string ReportID { get; set; }
        public string ReportName { get; set; }
        public string Name { get; set; }
        public string ReportDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string SubmitterEmail { get; set; }
        public decimal Amount { get; set; }
        public decimal ReimbursableAmount { get; set; }
        public decimal NonReimbursableAmount { get; set; }
    }
}
