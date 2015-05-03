USE [WindowsTasksPlanning]
GO

/****** Object:  Table [dbo].[ToDoActions]    Script Date: 02/05/2015 16:47:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ServicePlatform](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
    [CreationDate] [datetime] NULL,
    [ModificationDate] [datetime] NULL,
	[Product] [nvarchar](255) NULL,
	[TZ] [nvarchar](255) NULL,
	[IPAddress] [nvarchar](255) NULL,
    [DBName] [nvarchar](255) NULL,
    [IPAddressType] [nvarchar](255) NULL,
    [Login] [nvarchar](255) NULL,
    [Password] [nvarchar](255) NULL,
    [Contact] [nvarchar](255) NULL,
    [Retry_delay] [nvarchar](255) NULL,
    [LaunchAcquisitionHour] [datetime] NULL,
    [LaunchAcquisitionMinute] [datetime] NULL,
    [LastAcquisitionDate] [datetime] NULL,
    [LastAcquisitionStatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


