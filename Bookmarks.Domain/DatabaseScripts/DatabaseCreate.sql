/****** Object:  Table [dbo].[Bookmarks]    Script Date: 01/04/2011 11:34:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bookmarks]') AND type in (N'U'))
DROP TABLE [dbo].[Bookmarks]
GO
/****** Object:  Table [dbo].[BookmarkTags]    Script Date: 01/04/2011 11:34:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookmarkTags]') AND type in (N'U'))
DROP TABLE [dbo].[BookmarkTags]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 01/04/2011 11:34:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
DROP TABLE [dbo].[Tags]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 01/04/2011 11:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Tags] ON
INSERT [dbo].[Tags] ([TagID], [Name]) VALUES (1, N'Search engines')
INSERT [dbo].[Tags] ([TagID], [Name]) VALUES (2, N'big sites')
INSERT [dbo].[Tags] ([TagID], [Name]) VALUES (3, N'test')
SET IDENTITY_INSERT [dbo].[Tags] OFF
/****** Object:  Table [dbo].[BookmarkTags]    Script Date: 01/04/2011 11:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookmarkTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BookmarkTags](
	[BookmarkID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_BookmarkTags] PRIMARY KEY CLUSTERED 
(
	[BookmarkID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BookmarkTags] ([BookmarkID], [TagID]) VALUES (1, 1)
INSERT [dbo].[BookmarkTags] ([BookmarkID], [TagID]) VALUES (1, 2)
INSERT [dbo].[BookmarkTags] ([BookmarkID], [TagID]) VALUES (1, 3)
INSERT [dbo].[BookmarkTags] ([BookmarkID], [TagID]) VALUES (2, 3)
/****** Object:  Table [dbo].[Bookmarks]    Script Date: 01/04/2011 11:34:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bookmarks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Bookmarks](
	[BookmarkID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Url] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsPrivate] [bit] NOT NULL,
 CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED 
(
	[BookmarkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Bookmarks] ON
INSERT [dbo].[Bookmarks] ([BookmarkID], [Name], [Notes], [Url], [CreateDate], [IsPrivate]) VALUES (1, N'Google', NULL, N'www.google.com', CAST(0x00009E600083A374 AS DateTime), 0)
INSERT [dbo].[Bookmarks] ([BookmarkID], [Name], [Notes], [Url], [CreateDate], [IsPrivate]) VALUES (2, N'Yahoo', NULL, N'http://www.yahoo.com', CAST(0x00009CF400000000 AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Bookmarks] OFF
