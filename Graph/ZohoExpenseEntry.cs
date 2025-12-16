using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    public class ZohoExpenseEntry : PXGraph<ZohoExpenseEntry, ZohoExpense>
    {
        public SelectFrom<ZohoExpense>.View Header;
    }
}
