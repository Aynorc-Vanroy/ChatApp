USE [Chat]
GO
/****** Object:  Table [dbo].[chathis]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chathis](
	[UID1] [int] NULL,
	[UID2] [int] NULL,
	[isread] [bit] NULL,
	[Sendtime] [datetime] NULL,
	[Sendwords] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[frdreq]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[frdreq](
	[UID1] [int] NULL,
	[UID2] [int] NULL,
	[expgroup] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[friends]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[friends](
	[UID1] [int] NOT NULL,
	[UID2] [int] NULL,
	[groups] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupchathis]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupchathis](
	[GID] [int] NULL,
	[UID] [int] NULL,
	[Sendtime] [datetime] NULL,
	[Sendwords] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupmembers]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupmembers](
	[GID] [int] NULL,
	[UID] [int] NULL,
	[auth] [bit] NULL,
	[jointime] [datetime] NULL,
	[closetime] [datetime] NULL,
	[G_group] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupreq]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupreq](
	[GID] [int] NOT NULL,
	[UID] [int] NULL,
	[expgroup] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[GID] [int] IDENTITY(1,1) NOT NULL,
	[Groupname] [nvarchar](24) NULL,
	[CreatorUID] [int] NULL,
	[Groupsine] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[GID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 02/11/2024 15:50:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](24) NULL,
	[Sign] [nvarchar](255) NULL,
	[Groups] [nvarchar](255) NULL,
	[G_groups] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
