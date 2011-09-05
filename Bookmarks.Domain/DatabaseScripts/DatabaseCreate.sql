/****** Object:  ForeignKey [FK_Bookmarks_Users]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bookmarks_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bookmarks]'))
ALTER TABLE [dbo].[Bookmarks] DROP CONSTRAINT [FK_Bookmarks_Users]
GO
/****** Object:  ForeignKey [FK_BookmarkTags_Bookmarks]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Bookmarks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags] DROP CONSTRAINT [FK_BookmarkTags_Bookmarks]
GO
/****** Object:  ForeignKey [FK_BookmarkTags_Tags]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags] DROP CONSTRAINT [FK_BookmarkTags_Tags]
GO
/****** Object:  Table [dbo].[BookmarkTags]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookmarkTags]') AND type in (N'U'))
DROP TABLE [dbo].[BookmarkTags]
GO
/****** Object:  Table [dbo].[Bookmarks]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bookmarks]') AND type in (N'U'))
DROP TABLE [dbo].[Bookmarks]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 09/04/2011 21:48:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
DROP TABLE [dbo].[Tags]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/04/2011 21:48:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/04/2011 21:48:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Token] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 09/04/2011 21:48:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tags](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Bookmarks]    Script Date: 09/04/2011 21:48:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bookmarks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Bookmarks](
	[BookmarkId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Url] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED 
(
	[BookmarkId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[BookmarkTags]    Script Date: 09/04/2011 21:48:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookmarkTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BookmarkTags](
	[BookmarkId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[BookmarkTagId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BookmarkTags] PRIMARY KEY CLUSTERED 
(
	[BookmarkTagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  ForeignKey [FK_Bookmarks_Users]    Script Date: 09/04/2011 21:48:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bookmarks_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bookmarks]'))
ALTER TABLE [dbo].[Bookmarks]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bookmarks_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bookmarks]'))
ALTER TABLE [dbo].[Bookmarks] CHECK CONSTRAINT [FK_Bookmarks_Users]
GO
/****** Object:  ForeignKey [FK_BookmarkTags_Bookmarks]    Script Date: 09/04/2011 21:48:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Bookmarks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags]  WITH CHECK ADD  CONSTRAINT [FK_BookmarkTags_Bookmarks] FOREIGN KEY([BookmarkId])
REFERENCES [dbo].[Bookmarks] ([BookmarkId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Bookmarks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags] CHECK CONSTRAINT [FK_BookmarkTags_Bookmarks]
GO
/****** Object:  ForeignKey [FK_BookmarkTags_Tags]    Script Date: 09/04/2011 21:48:16 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags]  WITH CHECK ADD  CONSTRAINT [FK_BookmarkTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([TagId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookmarkTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookmarkTags]'))
ALTER TABLE [dbo].[BookmarkTags] CHECK CONSTRAINT [FK_BookmarkTags_Tags]
GO
