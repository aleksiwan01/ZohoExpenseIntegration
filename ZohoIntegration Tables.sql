USE [AIWMS24R1]
GO

/****** Object:  Table [dbo].[ZohoExpense] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZohoExpense]') AND type in (N'U'))
DROP TABLE [dbo].[ZohoExpense]
GO

/****** Object:  Table [dbo].[ZohoExpenseReport] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZohoExpenseReport]') AND type in (N'U'))
DROP TABLE [dbo].[ZohoExpenseReport]
GO

/****** Object:  Table [dbo].[ZohoSetup] 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZohoSetup]') AND type in (N'U'))
DROP TABLE [dbo].[ZohoSetup]
GO******/

CREATE TABLE [dbo].[ZohoExpense](
	[CompanyID] [int] NOT NULL,
	[ExpenseID] [int] IDENTITY(1,1) NOT NULL,
	[ZohoExpenseID] [nvarchar](50) NOT NULL,
	[ReportName] [nvarchar](200) NULL,
	[Date] [datetime] NULL,
	[CategoryName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[Amount] [decimal](38,2) NULL,
	[PaymentMode] [nvarchar](50) NULL,
	[PolicyName] [nvarchar](100) NULL,
	[DuplicateStatus] [bit] NULL,
	[CreatedByID] [uniqueidentifier] NOT NULL,
	[CreatedByScreenID] [char](8) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[LastModifiedByID] [uniqueidentifier] NOT NULL,
	[LastModifiedByScreenID] [char](8) NOT NULL,
	[LastModifiedDateTime] [datetime] NOT NULL,
	[Tstamp] [timestamp] NOT NULL,
	[NoteID] [uniqueidentifier] NOT NULL,
	CONSTRAINT PK_ZohoExpense PRIMARY KEY CLUSTERED(
	[CompanyID],
	[ExpenseID]
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[ZohoExpenseReport](
	[CompanyID] [int] NOT NULL,
	[ReportID] [int] IDENTITY(1,1) NOT NULL,
	[ReportName] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ReportDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [nvarchar](32) NULL,
	[SubmitterEmail] [nvarchar](100) NULL,
	[Amount] [decimal](38,2) NULL,
	[ReimbursableAmount] [decimal](38,2) NULL,
	[NonReimbursableAmount] [decimal](38,2) NULL,
	[CreatedByID] [uniqueidentifier] NOT NULL,
	[CreatedByScreenID] [char](8) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[LastModifiedByID] [uniqueidentifier] NOT NULL,
	[LastModifiedByScreenID] [char](8) NOT NULL,
	[LastModifiedDateTime] [datetime] NOT NULL,
	[Tstamp] [timestamp] NOT NULL,
	[NoteID] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_ZohoExpenseReport] PRIMARY KEY CLUSTERED(
	[CompanyID],
	[ReportID]
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


/* Keep this commented out after filling out initial data
CREATE TABLE [dbo].[ZohoSetup](
	[ClientID] [nvarchar](200) NOT NULL,
	[ClientSecret] [nvarchar](200) NOT NULL,
	[RefreshToken] [nvarchar](500) NOT NULL,
	[OrganizationID] [nvarchar](100) NOT NULL,
	[RedirectURI] [nvarchar](200) NOT NULL
)*/

