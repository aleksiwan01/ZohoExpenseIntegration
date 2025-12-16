using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webhooks
{
    [Serializable]
    public class ZohoExpenseReport : PXBqlTable, IBqlTable
    {
        public class PK : PrimaryKeyOf<ZohoExpenseReport>.By<reportName, name>
        {
            public static ZohoExpenseReport Find(PXGraph graph, reportName reportName, name name) => FindBy(graph, reportName, name);
        }

        public static class FK
        {

        }

        #region ReportID
        public abstract class reportID : BqlString.Field<reportID> { }
        [PXDBIdentity]
        [PXUIField(DisplayName = "ReportID")]
        public virtual int? ReportID { get; set; }
        #endregion

        #region Report Name
        public abstract class reportName : BqlString.Field<reportName> { }
        [PXDBString(200, IsKey = true, IsUnicode = true)]
        [PXUIField(DisplayName = "Report Name")]
        public virtual string ReportName { get; set; }
        #endregion

        #region Name
        public abstract class name : BqlString.Field<name> { }
        [PXDBString(100, IsKey = true, IsUnicode = true)]
        [PXUIField(DisplayName = "Submitting Employee Name")]
        public virtual string Name { get; set; }
        #endregion

        #region ReportDate
        public abstract class reportDate : BqlDateTime.Field<reportDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Report Date")]
        public virtual DateTime? ReportDate { get; set; }
        #endregion

        #region Start Date
        public abstract class startDate : BqlDateTime.Field<startDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Report Start Date")]
        public virtual DateTime? StartDate { get; set; }
        #endregion

        #region End Date
        public abstract class endDate : BqlDateTime.Field<endDate> { }
        [PXDBDate()]
        [PXUIField(DisplayName = "Report End Date")]
        public virtual DateTime? EndDate { get; set; }
        #endregion

        #region Status
        public abstract class status : BqlString.Field<status> { }
        [PXDBString()]
        [PXUIField(DisplayName = "Status")]
        public virtual string Status { get; set; }
        #endregion

        #region SubmitterEmail
        public abstract class submitterEmail : BqlString.Field<submitterEmail> { }
        [PXDBString()]
        [PXUIField(DisplayName = "Submitter Email")]
        public virtual string SubmitterEmail { get; set; }
        #endregion

        #region Amount
        public abstract class amount : BqlDecimal.Field<amount> { }
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Amount")]
        public virtual decimal? Amount { get; set; }
        #endregion

        #region ReimbursableAmount
        public abstract class reimbursableAmount : BqlDecimal.Field<reimbursableAmount> { }
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Reimbursable Amount")]
        public virtual decimal? ReimbursableAmount { get; set; }
        #endregion

        #region NonReimburasbleAmount
        public abstract class nonReimbursableAmount : BqlDecimal.Field<nonReimbursableAmount> { }
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Non-Reimbursable Amount")]
        public virtual decimal? NonReimbursableAmount { get; set; }
        #endregion

        #region CreatedByID
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        protected Guid? _CreatedByID;
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID
        {
            get
            {
                return this._CreatedByID;
            }
            set
            {
                this._CreatedByID = value;
            }
        }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        protected String _CreatedByScreenID;
        [PXDBCreatedByScreenID()]
        public virtual String CreatedByScreenID
        {
            get
            {
                return this._CreatedByScreenID;
            }
            set
            {
                this._CreatedByScreenID = value;
            }
        }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        protected DateTime? _CreatedDateTime;
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                this._CreatedDateTime = value;
            }
        }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        protected Guid? _LastModifiedByID;
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID
        {
            get
            {
                return this._LastModifiedByID;
            }
            set
            {
                this._LastModifiedByID = value;
            }
        }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        protected String _LastModifiedByScreenID;
        [PXDBLastModifiedByScreenID()]
        public virtual String LastModifiedByScreenID
        {
            get
            {
                return this._LastModifiedByScreenID;
            }
            set
            {
                this._LastModifiedByScreenID = value;
            }
        }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        protected DateTime? _LastModifiedDateTime;
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime
        {
            get
            {
                return this._LastModifiedDateTime;
            }
            set
            {
                this._LastModifiedDateTime = value;
            }
        }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.BQL.BqlByteArray.Field<Tstamp> { }
        protected Byte[] _tstamp;
        [PXDBTimestamp(VerifyTimestamp = VerifyTimestampOptions.BothFromGraphAndRecord)]
        public virtual Byte[] tstamp
        {
            get
            {
                return this._tstamp;
            }
            set
            {
                this._tstamp = value;
            }
        }
        #endregion

        #region NoteID
        public abstract class noteID : BqlGuid.Field<noteID> { }
        [PXNote]
        public virtual Guid? NoteID { get; set; }
        #endregion
    }
}
