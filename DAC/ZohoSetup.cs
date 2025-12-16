using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    public class ZohoSetup : PXBqlTable, IBqlTable
    {
        #region Client ID
        public abstract class clientID : PX.Data.BQL.BqlString.Field<clientID> { }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Client ID")]
        public virtual string ClientID { get; set; }
        #endregion

        #region Client Secret
        public abstract class clientSecret : PX.Data.BQL.BqlString.Field<clientSecret> { }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Client Secret")]
        public virtual string ClientSecret { get; set; }
        #endregion

        #region Refresh Token
        public abstract class refreshToken : PX.Data.BQL.BqlString.Field<refreshToken> { }
        [PXDBString(500, IsUnicode = true)]
        [PXUIField(DisplayName = "Refresh Token")]
        public virtual string RefreshToken { get; set; }
        #endregion

        #region Organization ID
        public abstract class organizationID : PX.Data.BQL.BqlString.Field<organizationID> { }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Organization ID")]
        public virtual string OrganizationID { get; set; }
        #endregion

        #region Redirect URI
        public abstract class redirectURI : PX.Data.BQL.BqlString.Field<redirectURI> { }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Redirect URI")]
        public virtual string RedirectURI { get; set; }
        #endregion
    }
}
