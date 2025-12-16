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
    public class ZohoExpense : PXBqlTable, IBqlTable
    {
        public class PK : PrimaryKeyOf<ZohoExpense>.By<expenseID>
        {
            public static ZohoExpense Find(PXGraph graph, string expenseID) => FindBy(graph, expenseID);
        }

        #region ExpenseID
        public abstract class expenseID : PX.Data.BQL.BqlInt.Field<expenseID> { }
        [PXDBIdentity]
        [PXUIField(DisplayName = "Expense ID")]
        public virtual int? ExpenseID { get; set; }
        #endregion
        #region ExpenseID
        public abstract class zohoExpenseID : PX.Data.BQL.BqlString.Field<zohoExpenseID> { }
        [PXDBString(50, IsKey = true, IsUnicode = true)]
        [PXSelector(typeof(ZohoExpense.zohoExpenseID), ValidateValue = false)]
        [PXUIField(DisplayName = "Expense ID")]
        public virtual string ZohoExpenseID { get; set; }
        #endregion

        #region ReportName
        public abstract class reportName : PX.Data.BQL.BqlString.Field<reportName> { }
        [PXDBString(200, IsUnicode = true)]
        [PXUIField(DisplayName = "Report Name")]
        public virtual string ReportName { get; set; }
        #endregion

        #region Date
        public abstract class date : PX.Data.BQL.BqlDateTime.Field<date> { }
        [PXDBDate]
        [PXUIField(DisplayName = "Date")]
        public virtual DateTime? Date { get; set; }
        #endregion

        #region CategoryName
        public abstract class categoryName : PX.Data.BQL.BqlString.Field<categoryName> { }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Category Name")]
        public virtual string CategoryName { get; set; }
        #endregion

        #region Description
        public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string Description { get; set; }
        #endregion

        #region Amount
        public abstract class amount : PX.Data.BQL.BqlDecimal.Field<amount> { }
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Amount")]
        public virtual decimal? Amount { get; set; }
        #endregion

        #region PaymentMode
        public abstract class paymentMode : PX.Data.BQL.BqlString.Field<paymentMode> { }
        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Payment Mode")]
        public virtual string PaymentMode { get; set; }
        #endregion

        #region PolicyName
        public abstract class policyName : PX.Data.BQL.BqlString.Field<policyName> { }
        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Policy Name")]
        public virtual string PolicyName { get; set; }
        #endregion

        #region DuplicateStatus
        public abstract class duplicateStatus : PX.Data.BQL.BqlBool.Field<duplicateStatus> { }
        [PXDBBool()]
        [PXUIField(DisplayName = "Duplicate Status")]
        public virtual bool? DuplicateStatus { get; set; }
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

        #region Selected
        public abstract class selected : BqlBool.Field<selected> { }
        [PXBool]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion
    }
}
