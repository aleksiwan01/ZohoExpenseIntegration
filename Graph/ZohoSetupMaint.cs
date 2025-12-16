using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    public class ZohoSetupMaint : PXGraph<ZohoSetupMaint>
    {
        public SelectFrom<ZohoSetup>.View Setup;
        public PXSave<ZohoSetup> Save;
        public PXCancel<ZohoSetup> Cancel;
    }
}
