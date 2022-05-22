CREATE TABLE [dbo].[tblDishCustomerCardInfo](
	[customerId] int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES tblDishCustomers(id),
	[customerLocality] nvarchar(100) NULL,
	[customerName] [nvarchar](100) NOT NULL,
	[customerPhone] [nvarchar](15) NOT NULL,
	[customerAddress] [nvarchar](200) NULL,
	[ownerName] [nvarchar](100)  NULL,
	[ownerPhone] [nvarchar](15)  NULL,
	[createdBy] [int] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[editedBy] [int] NULL,
	[editedDate] [datetime] NULL,
)

CREATE TABLE [dbo].[tblInternetCustomerCardInfo](
	[customerId] int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES tblInternetCustomers(id),
	[customerLocality] nvarchar(100) NULL,
	[customerName] [nvarchar](100) NOT NULL,
	[customerPhone] [nvarchar](15) NOT NULL,
	[customerAddress] [nvarchar](200) NULL,
	[ownerName] [nvarchar](100)  NULL,
	[ownerPhone] [nvarchar](15)  NULL,
	[createdBy] [int] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[editedBy] [int] NULL,
	[editedDate] [datetime] NULL,
)

--drop table [dbo].[tblInternetCustomerCardInfo]