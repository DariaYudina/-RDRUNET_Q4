USE [master]
GO
/****** Object:  Database [Library]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE DATABASE [Library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Library.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Library_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Library_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Library] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Library] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Library] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Library] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Library] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Library] SET ARITHABORT OFF 
GO
ALTER DATABASE [Library] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Library] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Library] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Library] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Library] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Library] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Library] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Library] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Library] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Library] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Library] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Library] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Library] SET  MULTI_USER 
GO
ALTER DATABASE [Library] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Library] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Library] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Library] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Library] SET QUERY_STORE = OFF
GO
USE [Library]
GO
/****** Object:  User [user_reader]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE USER [user_reader] FOR LOGIN [user_reader] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_librarian ]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE USER [user_librarian ] FOR LOGIN [user_librarian ] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_admin]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE USER [user_admin] FOR LOGIN [user_admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [reader]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE ROLE [reader]
GO
/****** Object:  DatabaseRole [librarian ]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE ROLE [librarian ]
GO
/****** Object:  DatabaseRole [admin]    Script Date: 2/21/2020 11:07:35 AM ******/
CREATE ROLE [admin]
GO
ALTER ROLE [reader] ADD MEMBER [user_reader]
GO
ALTER ROLE [librarian ] ADD MEMBER [user_librarian ]
GO
ALTER ROLE [admin] ADD MEMBER [user_admin]
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniqueBook]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[IsUniqueBook]
(
	@BookId int
)
RETURNS bit
AS
BEGIN
	DECLARE @isuniq bit = 1;
	DECLARE @Book  TABLE(Id INT, Title NVARCHAR(300), ISBN NVARCHAR(15), YearOfPublishing int)
	DECLARE @BookAuthors TABLE (AuthorId int)

	INSERT INTO  @Book (Id, Title, ISBN, YearOfPublishing)
	SELECT Id, Title, ISBN, YearOfPublishing
	FROM BookLibraryItems
	WHERE Id = @BookId

	INSERT INTO  @BookAuthors(AuthorId)
	SELECT Author_Id
	FROM LibraryAuthors 
	WHERE LibraryItem_Id = @BookId

	IF((SELECT ISBN FROM @Book) IS NOT NULL)
	BEGIN
		DECLARE @ISBN nvarchar(15) = (SELECT ISBN FROM @Book)
		IF(EXISTS(SELECT ISBN FROM BookLibraryItems WHERE ISBN = @ISBN  AND Id <> @BookId AND Deleted <>1))
			BEGIN
				SET @isuniq = 0
				RETURN @isuniq
			END
			ELSE
			BEGIN
				SET @isuniq = 1
				RETURN @isuniq
			END
	END
	IF(
	((SELECT Title FROM @Book) IS NOT NULL)
	AND ((SELECT YearOfPublishing FROM @Book) IS NOT NULL)
	AND (EXISTS(SELECT AuthorId FROM @BookAuthors))
	)
	BEGIN
		IF((EXISTS(SELECT * FROM @Book as book INNER JOIN BookLibraryItems as l ON book.Title = l.Title 
				   AND book.Id <> l.Id AND Deleted <>1))
			AND
			(EXISTS(SELECT * FROM @Book as book INNER JOIN BookLibraryItems as l ON book.YearOfPublishing = l.YearOfPublishing
			AND book.Id <> l.Id AND Deleted <>1))
		)
		BEGIN
			IF(EXISTS ((SELECT AuthorId FROM @BookAuthors)))
			BEGIN
				DECLARE @count INT = (SELECT MIN(LibraryItem_Id)FROM LibraryAuthors)
				WHILE(@count <= (SELECT MAX(LibraryItem_Id)FROM LibraryAuthors))
				BEGIN
					IF(NOT EXISTS(SELECT AuthorId FROM @BookAuthors
					EXCEPT
					SELECT Author_Id FROM LibraryAuthors WHERE LibraryItem_Id = @count AND LibraryItem_Id <> @BookId))
					BEGIN
						SET @isuniq = 0
						RETURN @isuniq
					END
					SET @count = @count + 1
				END
			END
		END
		ELSE
		SET @isuniq = 1
	END
	RETURN @isuniq
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniqueIssue]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[IsUniqueIssue] 
(
	@IssueId int
)
RETURNS bit
AS
BEGIN
	DECLARE @isuniq bit = 1;
	DECLARE @Issue  TABLE(Id INT, YearOfPublishing int, CountOfPublishing int, DateOfPublishing Datetime, 
						  PagesCount int, Commentary nvarchar(2000), NewspaperId int, City nvarchar(200), 
						  PublishingCompany nvarchar(300), ISSN nvarchar(50), Title nvarchar(300) )
	
	INSERT INTO  @Issue (Id, YearOfPublishing, CountOfPublishing, DateOfPublishing, 
						  PagesCount, Commentary, NewspaperId, City, 
						  PublishingCompany, ISSN, Title)
	SELECT Id, YearOfPublishing, CountOfPublishing, DateOfPublishing, PagesCount, Commentary,
			Newspaper_Id, City, PublishingCompany, ISSN, Title
	FROM IssueLibraryItems
	WHERE Id = @IssueId

	IF((SELECT ISSN FROM @Issue) IS NOT NULL)
	BEGIN
		DECLARE @ISSN nvarchar(50) = (SELECT ISSN FROM @Issue)
		DECLARE @Title nvarchar(300) = (SELECT Title FROM @Issue)
		IF(EXISTS(SELECT ISSN FROM IssueLibraryItems WHERE ISSN = @ISSN  AND Id <> @IssueId AND Title <> @Title
		 AND Deleted <> 1))
			BEGIN
				SET @isuniq = 0
				RETURN @isuniq
			END
			ELSE
			BEGIN
				SET @isuniq = 1
				RETURN @isuniq
			END
	END
	IF(((SELECT Title FROM @Issue) IS NOT NULL) AND ((SELECT DateOfPublishing FROM @Issue) IS NOT NULL)
	     AND ((SELECT PublishingCompany FROM @Issue) IS NOT NULL))
	BEGIN
		IF((EXISTS(SELECT * FROM @Issue as issue INNER JOIN IssueLibraryItems as l ON issue.Title = l.Title 
			AND issue.Id <> l.Id AND Deleted <> 1))
		AND
	   (EXISTS(SELECT * FROM @Issue as issue INNER JOIN IssueLibraryItems as l ON issue.DateOfPublishing = l.DateOfPublishing
			   AND issue.Id <> l.Id AND Deleted <> 1))
	   AND
	   (EXISTS(SELECT * FROM @Issue as issue INNER JOIN IssueLibraryItems as l ON issue.PublishingCompany = l.PublishingCompany 
			    AND issue.Id <> l.Id AND Deleted <> 1))
	   )
		BEGIN
			SET @isuniq = 0
			RETURN @isuniq
		END
	END
	RETURN @isuniq
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniquePatent]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[IsUniquePatent]
(
	@PatentId int
)
RETURNS bit
AS
BEGIN
	DECLARE @isuniq bit = 1;
	DECLARE @Patent  TABLE(Id INT, Title nvarchar(300), PagesCount int, Commentary nvarchar(2000), Country nvarchar(200), 
						RegistrationNumber nvarchar(50), ApplicationDate datetime, PublicationDate datetime)
	
	INSERT INTO  @Patent (Id, Title, PagesCount, Commentary, Country, 
						RegistrationNumber, ApplicationDate, PublicationDate)
	SELECT Id, Title, PagesCount, Commentary, Country, 
						RegistrationNumber, ApplicationDate, PublicationDate
	FROM PatentLibraryItems
	WHERE Id = @PatentId

	IF(((SELECT RegistrationNumber FROM @Patent) IS NOT NULL) AND ((SELECT Country FROM @Patent) IS NOT NULL))
	BEGIN
		IF((EXISTS(SELECT * FROM @Patent as patent INNER JOIN PatentLibraryItems as l 
		ON patent.RegistrationNumber = l.Title AND patent.Id <> l.Id))
		AND
	   (EXISTS(SELECT * FROM @Patent as patent INNER JOIN PatentLibraryItems as l 
		ON patent.Country = l.Country AND patent.Id <> l.Id)))
		BEGIN
			SET @isuniq = 0
			RETURN @isuniq
		END
	END
	RETURN @isuniq
END

GO
/****** Object:  Table [dbo].[Books]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] NOT NULL,
	[City] [nvarchar](200) NULL,
	[PublishingCompany] [nvarchar](300) NULL,
	[ISBN] [nvarchar](50) NULL,
	[YearOfPublishing] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](300) NULL,
	[PagesCount] [int] NULL,
	[Commentary] [nvarchar](2000) NULL,
	[LibraryType] [nvarchar](50) NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_LibraryItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BookLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BookLibraryItems]
AS
SELECT        dbo.LibraryItems.*, dbo.Books.City, dbo.Books.PublishingCompany, dbo.Books.ISBN, dbo.Books.YearOfPublishing
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Books ON dbo.LibraryItems.Id = dbo.Books.Id
GO
/****** Object:  Table [dbo].[Patents]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patents](
	[Id] [int] NOT NULL,
	[Country] [nvarchar](200) NULL,
	[RegistrationNumber] [nvarchar](50) NULL,
	[ApplicationDate] [datetime] NULL,
	[PublicationDate] [datetime] NULL,
 CONSTRAINT [PK_Patent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PatentLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatentLibraryItems]
AS
SELECT        dbo.LibraryItems.*, dbo.Patents.Country, dbo.Patents.RegistrationNumber, dbo.Patents.ApplicationDate, dbo.Patents.PublicationDate
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Patents ON dbo.LibraryItems.Id = dbo.Patents.Id
GO
/****** Object:  Table [dbo].[Issues]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issues](
	[Id] [int] NOT NULL,
	[Newspaper_Id] [int] NULL,
	[CountOfPublishing] [int] NULL,
	[DateOfPublishing] [datetime] NULL,
	[YearOfPublishing] [int] NULL,
 CONSTRAINT [PK_Newspaper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Newspapers]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Newspapers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](300) NULL,
	[City] [nvarchar](200) NULL,
	[PublishingCompany] [nvarchar](300) NULL,
	[ISSN] [nvarchar](50) NULL,
	[YearOfPublishing] [int] NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[IssueLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IssueLibraryItems]
AS
SELECT        dbo.LibraryItems.Id, dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.LibraryItems.Deleted, dbo.Issues.Newspaper_Id, dbo.Issues.CountOfPublishing, 
                         dbo.Issues.DateOfPublishing, dbo.Issues.YearOfPublishing, dbo.Newspapers.City, dbo.Newspapers.PublishingCompany, dbo.Newspapers.ISSN, dbo.LibraryItems.Title
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Issues ON dbo.LibraryItems.Id = dbo.Issues.Id INNER JOIN
                         dbo.Newspapers ON dbo.Issues.Newspaper_Id = dbo.Newspapers.Id
GO
/****** Object:  Table [dbo].[LibraryAuthors]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryAuthors](
	[LibraryItem_Id] [int] NOT NULL,
	[Author_Id] [int] NOT NULL,
 CONSTRAINT [PK_LibraryAuthors] PRIMARY KEY CLUSTERED 
(
	[LibraryItem_Id] ASC,
	[Author_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](200) NULL,
	[Type] [nvarchar](50) NULL,
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AuthorLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AuthorLibraryItems]
AS
SELECT        dbo.LibraryAuthors.*, dbo.Authors.*
FROM            dbo.LibraryAuthors INNER JOIN
                         dbo.Authors ON dbo.LibraryAuthors.Author_Id = dbo.Authors.Id
GO
/****** Object:  View [dbo].[AllLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllLibraryItems]
AS
SELECT        dbo.Books.City, dbo.Books.PublishingCompany, dbo.Books.ISBN, dbo.Books.YearOfPublishing AS BooksYearOfPublishing, dbo.Issues.Newspaper_Id, dbo.Issues.CountOfPublishing, dbo.Issues.DateOfPublishing, 
                         dbo.Issues.YearOfPublishing AS IssueYearOfPublishing, dbo.LibraryItems.Id, dbo.LibraryItems.Title, dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.Patents.Country, 
                         dbo.Patents.RegistrationNumber, dbo.Patents.ApplicationDate, dbo.Patents.PublicationDate, dbo.Newspapers.ISSN, dbo.Newspapers.YearOfPublishing AS NewspapersYearOfPublishing, 
                         dbo.Newspapers.PublishingCompany AS NewspapersPublishingCompany, dbo.Newspapers.City AS NewspapersCity, dbo.Newspapers.Title AS NewspapersTitle
FROM            dbo.Newspapers LEFT OUTER JOIN
                         dbo.Issues ON dbo.Newspapers.Id = dbo.Issues.Newspaper_Id RIGHT OUTER JOIN
                         dbo.LibraryItems LEFT OUTER JOIN
                         dbo.Books ON dbo.LibraryItems.Id = dbo.Books.Id ON dbo.Issues.Id = dbo.LibraryItems.Id LEFT OUTER JOIN
                         dbo.Patents ON dbo.LibraryItems.Id = dbo.Patents.Id
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NULL,
	[ObjectType] [nvarchar](200) NULL,
	[ObjectId] [int] NULL,
	[Desciption] [nvarchar](300) NULL,
	[UserName] [nvarchar](200) NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (319, N'John', NULL, NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (320, N'John', N'Test2', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (321, N'John', N'Test3', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (322, N'John', N'Test4', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (323, N'John', N'est5', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (324, N'John', N'Test1', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (325, N'John', N'Test2', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (326, N'John', N'Test3', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (327, N'John', N'Test4', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (328, N'John', N'est5', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (329, N'John', N'Test1', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (330, N'John', N'Test2', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (331, N'John', N'Test3', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (332, N'John', N'Test4', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (333, N'John', N'est5', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (334, N'John', N'Test1', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (335, N'John', N'Test2', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (336, N'John', N'Test3', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (337, N'John', N'Test4', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (338, N'John', N'est5', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (340, N'John', N'Test2', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (341, N'John', N'Test3', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (342, N'John', N'Test4', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (343, N'John', N'est5', NULL, NULL)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (344, N'name', N'name', NULL, 0)
SET IDENTITY_INSERT [dbo].[Authors] OFF
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (72, N'ggd', N'hello', N'gfdgfd', 1997)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (81, N'ffds', N'h', N'fdsfds', 2000)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (82, N'ddd', N'hi', N'daasa', 1800)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (91, N'rrrr', N'rrrr', N'rrrr', 1990)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (97, N'fdsfs', N'fdsfs', N'fsdsfd', 1999)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN], [YearOfPublishing]) VALUES (99, N'dsads', N'dasads', N'dsaads', 2000)
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing], [YearOfPublishing]) VALUES (102, 1, 3, CAST(N'1996-01-01T00:00:00.000' AS DateTime), 1996)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (72, 319)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (81, 319)
SET IDENTITY_INSERT [dbo].[LibraryItems] ON 

INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (72, N'hello', 200, N'hello', N'book', 1)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (81, N'rrrr', 200, N'hello', N'book', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (82, N'dsdsa', 200, N'comment', N'Book', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (91, N'rrrr', 100, N'rrrr', N'rrrr', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (97, N'dssd', 200, N'ssd', N'fdssdf', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (98, N'dssd', 200, N'ssd', N'fdssdf', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (99, N'dsads', 100, N'sadads', N'dsaads', 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (101, N'patent', 199, N'patent', N'patent', NULL)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [Deleted]) VALUES (102, N'issue', 299, N'issue', N'issue', NULL)
SET IDENTITY_INSERT [dbo].[LibraryItems] OFF
SET IDENTITY_INSERT [dbo].[Logs] ON 

INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (107, CAST(N'2020-02-19T14:06:58.757' AS DateTime), N'hello', 75, N'book item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (109, CAST(N'2020-02-19T14:09:20.770' AS DateTime), N'hello', 75, N'book item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (110, CAST(N'2020-02-19T14:24:14.857' AS DateTime), N'hello', 75, N'book item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (111, CAST(N'2020-02-19T14:27:14.360' AS DateTime), N'hello', 75, N'book item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (112, CAST(N'2020-02-19T14:29:16.840' AS DateTime), N'book', 72, N'Attempt to delete an book item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (113, CAST(N'2020-02-19T14:42:10.250' AS DateTime), NULL, 78, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (114, CAST(N'2020-02-19T14:43:16.910' AS DateTime), NULL, 78, N'Attempt to delete an issue item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (115, CAST(N'2020-02-19T14:43:34.923' AS DateTime), NULL, 78, N'Attempt to delete an issue item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (116, CAST(N'2020-02-19T14:45:26.263' AS DateTime), N'issue', 78, N'Attempt to delete an issue item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (117, CAST(N'2020-02-19T14:47:49.150' AS DateTime), N'issue', 78, N'issue item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (118, CAST(N'2020-02-19T14:56:08.860' AS DateTime), NULL, 79, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (119, CAST(N'2020-02-19T14:56:51.830' AS DateTime), N'patent', 79, N'Attempt to delete an patent item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (120, CAST(N'2020-02-19T14:58:56.097' AS DateTime), N'patent', 79, N'Attempt to delete an issue item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (121, CAST(N'2020-02-19T15:03:35.390' AS DateTime), N'patent', 79, N'Attempt to delete an patent item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (122, CAST(N'2020-02-19T15:04:10.293' AS DateTime), N'patent', 79, N'patent item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (123, CAST(N'2020-02-19T15:06:14.097' AS DateTime), NULL, 80, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (124, CAST(N'2020-02-19T15:06:45.663' AS DateTime), NULL, 80, N'Attempt to delete an patent item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (125, CAST(N'2020-02-19T15:08:56.553' AS DateTime), NULL, 80, N'Attempt to delete an patent item by librarian', N'user_librarian ')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (126, CAST(N'2020-02-19T15:09:55.733' AS DateTime), NULL, 80, N'patent item was deleted by administrator', N'user_admin')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (127, CAST(N'2020-02-20T12:37:45.160' AS DateTime), NULL, 81, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (128, CAST(N'2020-02-20T15:39:49.217' AS DateTime), N'Book', 82, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (129, CAST(N'2020-02-20T15:39:49.237' AS DateTime), N'Book', 82, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (132, CAST(N'2020-02-20T20:21:55.633' AS DateTime), N'Book', 84, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (134, CAST(N'2020-02-20T20:24:37.583' AS DateTime), N'Book', 85, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (140, CAST(N'2020-02-20T20:49:29.613' AS DateTime), N'rrrr', 91, N'rrrr was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (141, CAST(N'2020-02-20T20:49:29.633' AS DateTime), N'rrrr', 91, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (142, CAST(N'2020-02-20T21:13:53.353' AS DateTime), N'author', 344, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (148, CAST(N'2020-02-20T21:24:54.467' AS DateTime), N'fdssdf', 97, N'fdssdf was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (149, CAST(N'2020-02-20T21:26:19.160' AS DateTime), N'fdssdf', 98, N'fdssdf was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (150, CAST(N'2020-02-20T21:28:36.343' AS DateTime), N'dsaads', 99, N'dsaads was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (151, CAST(N'2020-02-20T21:28:36.357' AS DateTime), N'dsaads', 99, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (153, CAST(N'2020-02-21T05:10:20.920' AS DateTime), NULL, 101, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (154, CAST(N'2020-02-21T05:51:03.997' AS DateTime), N'issue', 102, N'issue was added', N'dbo')
SET IDENTITY_INSERT [dbo].[Logs] OFF
SET IDENTITY_INSERT [dbo].[Newspapers] ON 

INSERT [dbo].[Newspapers] ([Id], [Title], [City], [PublishingCompany], [ISSN], [YearOfPublishing]) VALUES (1, N'newspaper', N'newspaper', N'newspaper', N'newspaper', 2000)
SET IDENTITY_INSERT [dbo].[Newspapers] OFF
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (101, N'county', N'numbe', CAST(N'1996-01-01T00:00:00.000' AS DateTime), CAST(N'2000-02-02T00:00:00.000' AS DateTime))
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_DateAndTimeOfOperation]  DEFAULT (getdate()) FOR [Time]
GO
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_UserName]  DEFAULT (user_name()) FOR [UserName]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_LibraryItems1] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_LibraryItems1]
GO
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Newspapers_LibraryItems] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Newspapers_LibraryItems]
GO
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Newspapers_NewspaperIssues] FOREIGN KEY([Newspaper_Id])
REFERENCES [dbo].[Newspapers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Newspapers_NewspaperIssues]
GO
ALTER TABLE [dbo].[LibraryAuthors]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAuthors_Authors] FOREIGN KEY([Author_Id])
REFERENCES [dbo].[Authors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LibraryAuthors] CHECK CONSTRAINT [FK_LibraryAuthors_Authors]
GO
ALTER TABLE [dbo].[LibraryAuthors]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAuthors_LibraryItems] FOREIGN KEY([LibraryItem_Id])
REFERENCES [dbo].[LibraryItems] ([Id])
GO
ALTER TABLE [dbo].[LibraryAuthors] CHECK CONSTRAINT [FK_LibraryAuthors_LibraryItems]
GO
ALTER TABLE [dbo].[Patents]  WITH CHECK ADD  CONSTRAINT [FK_Patents_LibraryItems] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
GO
ALTER TABLE [dbo].[Patents] CHECK CONSTRAINT [FK_Patents_LibraryItems]
GO
/****** Object:  StoredProcedure [dbo].[AddAuthor]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAuthor]
	@Id int output,
	@FirstName nvarchar(300), 
	@LastName nvarchar(300)
AS
BEGIN
BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		INSERT INTO Authors(FirstName, LastName, Deleted)
		VALUES(@FirstName, @LastName, 0)
		SET @Id = @@IDENTITY;

		DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id)
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was added')

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddAuthors]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddAuthors]
	@listAuthors nvarchar(MAX),
	@JsonAuthorsId nvarchar(MAX) output
AS
BEGIN


BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		DECLARE @AuthorsId TABLE (Id INT)
		INSERT INTO Authors(FirstName, LastName, Deleted)
			OUTPUT INSERTED.Id
			INTO @AuthorsId
		SELECT firstname, lastname, 0
		FROM OPENJSON(@listAuthors)
		  WITH (
			firstName NVARCHAR(50) '$.firstName',
			lastName NVARCHAR(200) '$.lastName'
		  );
		SET @JsonAuthorsId = (
		SELECT Id  
		FROM @AuthorsId  
		FOR JSON PATH
		);
		DECLARE @count INT = (SELECT MIN(Id)FROM @AuthorsId)
		WHILE(@count <= (SELECT MAX(Id)FROM @AuthorsId))
		BEGIN
			INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ( N'author', @count,  N'item was added')
			SET @count = @count + 1;
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[AddBook]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBook]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@City nvarchar(200),
	@PublishingCompany nvarchar(200),
	@ISBN nvarchar(200),
	@YearOfPublishing int,

	@listAuthorsId nvarchar(MAX)
AS
BEGIN
	SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN


		EXEC AddBookInTable @Id OUTPUT, @Title, @PagesCount, @Commentary, @LibraryType, @City,
							@PublishingCompany, @ISBN, @YearOfPublishing;
		IF(@Id IS NULL) ROLLBACK TRANSACTION

		IF( (SELECT dbo.IsUniqueBook(@Id)) = 0) ROLLBACK TRANSACTION

		IF(@listAuthorsId IS NOT NULL AND @listAuthorsId <> N'[]')
		BEGIN
			EXEC AddRelationshipBookAuthors @listAuthorsId , @Id; 
			IF(@Id IS NULL) ROLLBACK TRANSACTION
		END
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id ,  N'item was added')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION
		  SET @Id = -1;
		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddBookInTable]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddBookInTable]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@City nvarchar(200),
	@PublishingCompany nvarchar(200),
	@ISBN nvarchar(200),
	@YearOfPublishing int
AS
BEGIN
		INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, Deleted)
		VALUES(@Title, @PagesCount, @Commentary, @LibraryType, 0)

		SET @Id = SCOPE_IDENTITY();
		INSERT INTO Books(Id, City, PublishingCompany, ISBN, YearOfPublishing)
		VALUES (@Id, @City, @PublishingCompany, @ISBN,  @YearOfPublishing)
END
GO
/****** Object:  StoredProcedure [dbo].[AddIssue]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddIssue] 
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Newspaper_Id int,
	@CountOfPublishing int,
	@DateOfPublishing datetime,
	@YearOfPublishing int
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		IF(NOT EXISTS (SELECT Id FROM Newspapers WHERE Id = @Newspaper_Id)) ROLLBACK TRANSACTION

		EXEC AddIssueInTable @Id OUTPUT, @Title, @PagesCount, @Commentary, @LibraryType, 
							 @Newspaper_Id, @CountOfPublishing, @DateOfPublishing, @YearOfPublishing;
		IF(@Id IS NULL) ROLLBACK TRANSACTION

		IF( (SELECT dbo.IsUniqueIssue(@Id)) = 0) ROLLBACK TRANSACTION
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was added')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddIssueInTable]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddIssueInTable]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Newspaper_Id int,
	@CountOfPublishing int,
	@DateOfPublishing datetime,
	@YearOfPublishing int
	
AS
BEGIN
	INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, Deleted)
	VALUES(@Title, @PagesCount, @Commentary, @LibraryType, 0)
	SET @Id = @@IDENTITY;
	INSERT INTO Issues(Id, Newspaper_Id, CountOfPublishing, DateOfPublishing, YearOfPublishing)
	VALUES (@Id, @Newspaper_Id, @CountOfPublishing, @DateOfPublishing,  @YearOfPublishing)
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewspaper]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewspaper]
	@Id int output,
	@Title nvarchar(300),
	@City nvarchar(200),
	@PublishingCompany nvarchar(300),
	@ISSN nvarchar(13),
	@YearOfPublishing int
AS
BEGIN
BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		INSERT INTO Newspapers(Title, City, PublishingCompany, ISSN, YearOfPublishing)
		VALUES(@Title, @City, @PublishingCompany, @ISSN, @YearOfPublishing)
		SET @Id = @@IDENTITY;
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ( N'Newspaper', @Id,  N'item was added')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddPatent]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddPatent]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Country nvarchar(200),
	@RegistrationNumber nvarchar(50),
	@ApplicationDate datetime,
	@PublicationDate datetime,

	@listAuthorsId nvarchar(MAX)
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN

		EXEC AddPatentInTable @Id OUTPUT, @Title, @PagesCount, @Commentary, @LibraryType, 
							  @Country, @RegistrationNumber, @ApplicationDate, @PublicationDate;
		IF(@Id IS NULL) ROLLBACK TRANSACTION

		IF( (SELECT dbo.IsUniquePatent(@Id)) = 0) ROLLBACK TRANSACTION

		IF(@listAuthorsId IS NOT NULL AND @listAuthorsId <> N'[]')
		BEGIN
			EXEC AddRelationshipBookAuthors @listAuthorsId , @Id; 
			IF(@Id IS NULL) ROLLBACK TRANSACTION
		END

		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was added')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddPatentInTable]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPatentInTable]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Country nvarchar(200),
	@RegistrationNumber nvarchar(9),
	@ApplicationDate datetime,
	@PublicationDate datetime
AS
BEGIN
	INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, Deleted)
	VALUES(@Title, @PagesCount, @Commentary, @LibraryType, 0)
	SET @Id = @@IDENTITY;
	INSERT INTO Patents( Id, Country, RegistrationNumber, ApplicationDate, PublicationDate)
	VALUES (@Id, @Country, @RegistrationNumber, @ApplicationDate, @PublicationDate)
END
GO
/****** Object:  StoredProcedure [dbo].[AddRelationshipBookAuthors]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddRelationshipBookAuthors]
	@listAuthors nvarchar(MAX),
	@bookId int
AS
BEGIN
	INSERT INTO LibraryAuthors(LibraryItem_Id, Author_Id)
	SELECT @bookId, Id
	FROM OPENJSON(@listAuthors)
	  WITH (
		Id int '$.Id'
	  );
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthor]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAuthor]
@Id int,
@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		IF(IS_MEMBER ('librarian') = 1)
		BEGIN
			UPDATE Authors SET Deleted = 1 WHERE Id = @Id
			INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (N'author', @Id,  N'Attempt to delete an author item by librarian')
		END
		IF(IS_MEMBER ('admin') = 1 OR IS_MEMBER ('dbo') = 1)
		BEGIN
		DELETE FROM Authors
		WHERE Id = @Id
		SET @CntDeleteRow = @@ROWCOUNT
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (N'author', @Id,  N'author item was deleted by administrator')
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthors]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAuthors]
	@BookId int
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		DELETE Authors
		FROM LibraryAuthors
		INNER JOIN Authors ON LibraryAuthors.Author_Id = Authors.Id
		WHERE LibraryAuthors.LibraryItem_Id = @BookId;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBook]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteBook]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id)
			IF(IS_MEMBER ('librarian') = 1)
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'Attempt to delete an book item by librarian')
			END
			IF(IS_MEMBER ('admin') = 1 OR IS_MEMBER ('dbo') = 1)
			BEGIN
				DELETE FROM Books
				WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1) ROLLBACK TRANSACTION

				DELETE FROM LibraryAuthors
				WHERE LibraryItem_Id = @Id

				DELETE FROM LibraryItems WHERE Id =  @Id
				SET @CntDeleteRow = @@ROWCOUNT

				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'book item was deleted by administrator')
			END
			COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteIssue]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteIssue]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id)
			IF(IS_MEMBER ('librarian') = 1)
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'Attempt to delete an issue item by librarian')
			END
			IF(IS_MEMBER ('admin') = 1 OR IS_MEMBER ('dbo') = 1)
			BEGIN
				DELETE FROM Issues
				WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1) ROLLBACK TRANSACTION

				DELETE FROM LibraryItems WHERE Id =  @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1) ROLLBACK TRANSACTION

				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'issue item was deleted by administrator')
			END
		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteLibraryItem]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLibraryItem]
@Id int
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM LibraryAuthors WHERE LibraryItem_Id = @Id
			DELETE FROM LibraryItems WHERE Id =  @Id
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteNewspaper]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteNewspaper]
	@Id int, 
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		DELETE FROM Newspapers
		WHERE Id = @Id
		SET @CntDeleteRow = @@ROWCOUNT
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeletePatent]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeletePatent]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id)
			IF(IS_MEMBER ('librarian') = 1)
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'Attempt to delete an patent item by librarian')
			END
			IF(IS_MEMBER ('admin') = 1 OR IS_MEMBER ('dbo') = 1)
			BEGIN
			DELETE FROM Patents
			WHERE Id = @Id
			SET @CntDeleteRow = @@ROWCOUNT
			IF(@CntDeleteRow <> 1) ROLLBACK TRANSACTION

			DELETE FROM LibraryItems WHERE Id =  @Id
			SET @CntDeleteRow = @@ROWCOUNT
			IF(@CntDeleteRow <> 1) ROLLBACK TRANSACTION
			INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'patent item was deleted by administrator')
			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLibraryItems]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllLibraryItems]
AS
BEGIN
   EXEC GetBooks
   EXEC GetPatents
   EXEC GetIssues
END

GO
/****** Object:  StoredProcedure [dbo].[GetBookById]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBookById]
	@Id int
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
		( SELECT Author_Id as Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM BookLibraryItems as t
	WHERE t.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooks]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBooks] 
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
		( SELECT Author_Id as Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM BookLibraryItems as t
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByAuthor]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBooksByAuthor] 
@AuthorId int
AS
BEGIN
	DECLARE @SearchedBooksId TABLE(BookId int) 
	INSERT INTO @SearchedBooksId(BookId)
	SELECT LibraryItem_Id FROM LibraryAuthors WHERE Author_Id = @AuthorId
    IF(EXISTS ((SELECT BookId FROM @SearchedBooksId)))
	BEGIN
		DECLARE @count INT = (SELECT MIN(BookId) FROM @SearchedBooksId)
		WHILE(@count <= (SELECT MAX(BookId) FROM @SearchedBooksId))
		BEGIN
			 SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, 
					YearOfPublishing, 
					( SELECT Author_Id, Firstname, Lastname
					FROM AuthorLibraryItems 
					WHERE LibraryItem_Id = t.Id 
					FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
			FROM BookLibraryItems as t
			WHERE Id = @count
			SET @count = @count + 1
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByPublishingCompanyStartsWithInputText]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[GetBooksByPublishingCompanyStartsWithInputText] 
	@PublishingCompany nvarchar(300)
AS
BEGIN
	SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
	( SELECT Author_Id, Firstname, Lastname
	FROM AuthorLibraryItems 
	WHERE LibraryItem_Id = t.Id
	FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM BookLibraryItems as t 
	WHERE PublishingCompany LIKE ''+ @PublishingCompany + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[GetIssues]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetIssues]
AS
BEGIN
    SELECT Id, PagesCount, Commentary, 
	       CountOfPublishing, DateOfPublishing, YearOfPublishing, 
		   (SELECT TOP(1)Id, Title, City, PublishingCompany, ISSN as Issn
		   FROM Newspapers 
		   WHERE Id = issue.Newspaper_Id
		   FOR JSON PATH, INCLUDE_NULL_VALUES) 
		   as Newspaper
    FROM IssueLibraryItems issue
END
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItemsByTitle]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLibraryItemsByTitle]
	@Title nvarchar(300)
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
	( SELECT Author_Id as Id, Firstname, Lastname
	FROM AuthorLibraryItems 
	WHERE LibraryItem_Id = t.Id 
	FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
	FROM BookLibraryItems as t
	WHERE Title = @Title

	SELECT Id, Title, PagesCount, Commentary, 
		Country, ApplicationDate, PublicationDate, RegistrationNumber, 
				( SELECT Author_Id, Firstname, Lastname
				FROM AuthorLibraryItems 
				WHERE LibraryItem_Id = t.Id 
				FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM PatentLibraryItems as t 
	WHERE Title = @Title

    SELECT Id, PagesCount, Commentary, 
	       CountOfPublishing, DateOfPublishing, YearOfPublishing, 
		   (SELECT TOP(1)Id, Title, City, PublishingCompany, ISSN as Issn
		   FROM Newspapers 
		   WHERE Id = issue.Newspaper_Id
		   FOR JSON PATH, INCLUDE_NULL_VALUES) 
		   as Newspaper
    FROM IssueLibraryItems issue
	WHERE Title = @Title
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapers]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNewspapers]
AS
BEGIN
	SELECT Id, Title, City, PublishingCompany, ISSN,
	YearOfPublishing
	FROM Newspapers 
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapersById]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNewspapersById] 
	@Id int 
AS
BEGIN
	SELECT Id, Title, City, PublishingCompany, ISSN, YearOfPublishing
	FROM Newspapers
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetPatentByAuthor]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPatentByAuthor]
@AuthorId int
AS
BEGIN
	DECLARE @SearchedPatentsId TABLE(PatentId int) 
	INSERT INTO @SearchedPatentsId(PatentId)
	SELECT LibraryItem_Id FROM LibraryAuthors WHERE Author_Id = @AuthorId
    IF(EXISTS ((SELECT PatentId FROM @SearchedPatentsId)))
	BEGIN
		DECLARE @count INT = (SELECT MIN(PatentId) FROM @SearchedPatentsId)
		WHILE(@count <= (SELECT MAX(PatentId) FROM @SearchedPatentsId))
		BEGIN
			 SELECT Id, Title, PagesCount, Commentary, Country, ApplicationDate, 
					PublicationDate, RegistrationNumber, 
					( SELECT Author_Id, Firstname, Lastname
					FROM AuthorLibraryItems 
					WHERE LibraryItem_Id = t.Id 
					FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
			FROM PatentLibraryItems as t
			WHERE Id = @count
			SET @count = @count + 1
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[GetPatents]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatents]
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, 
		   Country, ApplicationDate, PublicationDate, RegistrationNumber, 
					( SELECT Author_Id, Firstname, Lastname
					FROM AuthorLibraryItems 
					WHERE LibraryItem_Id = t.Id 
					FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM PatentLibraryItems as t
END
GO
/****** Object:  StoredProcedure [dbo].[GetSortedLibraryItemsByYearDesc]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSortedLibraryItemsByYearDesc]
AS
BEGIN
	SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
	( SELECT Author_Id, Firstname, Lastname
	FROM AuthorLibraryItems 
	WHERE LibraryItem_Id = t.Id
	FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM BookLibraryItems as t
	ORDER BY YearOfPublishing DESC
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAuthor]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateAuthor]
	@Id int,
	@FirstName nvarchar(300), 
	@LastName nvarchar(300),

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN

		UPDATE Authors
		SET 
		FirstName = @FirstName,
		LastName = @LastName
		WHERE Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (N'author', @Id,  N'item was updated' )
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateBook]
	@Id int,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@City nvarchar(200),
	@PublishingCompany nvarchar(200),
	@ISBN nvarchar(200),
	@YearOfPublishing int,

	@AuthorsId nvarchar(max),

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		UPDATE LibraryItems
		SET 
		Title = @Title,
		PagesCount = @PagesCount,
		Commentary = @Commentary,
		LibraryType = @LibraryType
		WHERE Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		UPDATE Books
		SET 
		City = @City,
		PublishingCompany = @PublishingCompany,
		ISBN = @ISBN,
		YearOfPublishing = @YearOfPublishing
		WHERE Books.Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow < 1) ROLLBACK TRANSACTION

		DELETE FROM LibraryAuthors
		WHERE LibraryItem_Id = @id
		
		IF(@AuthorsId IS NOT NULL)
		BEGIN
			EXEC AddRelationshipBookAuthors @AuthorsId , @Id; 
			IF(@Id IS NULL) ROLLBACK TRANSACTION
		END

		IF( (SELECT dbo.IsUniqueBook(@Id)) = 0) ROLLBACK TRANSACTION
		SET @CntUpdateRow = @@ROWCOUNT
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was updated' )
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateIssue]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateIssue]
	@Id int,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Newspaper_Id int,
	@CountOfPublishing int,
	@DateOfPublishing datetime,
	@YearOfPublishing int,

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		UPDATE LibraryItems
		SET 
		Title = @Title,
		PagesCount = @PagesCount,
		Commentary = @Commentary,
		LibraryType = @LibraryType
		WHERE Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		UPDATE Issues
		SET 
		Newspaper_Id = @Newspaper_Id,
		CountOfPublishing = @CountOfPublishing,
		DateOfPublishing = @DateOfPublishing,
		YearOfPublishing = @YearOfPublishing
		WHERE Issues.Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow < 1) ROLLBACK TRANSACTION
		
		IF( (SELECT dbo.IsUniqueIssue(@Id)) = 0) ROLLBACK TRANSACTION
		SET @CntUpdateRow = @@ROWCOUNT

		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was updated' )
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateNewspaper]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateNewspaper]
	@Id int output,
	@Title nvarchar(300),
	@City nvarchar(200),
	@PublishingCompany nvarchar(300),
	@ISSN nvarchar(13),
	@YearOfPublishing datetime,

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN

		UPDATE Newspapers
		SET 
		Title = @Title,
		City = @City,
		PublishingCompany = @PublishingCompany,
		ISSN = @ISSN,
		YearOfPublishing = @YearOfPublishing
		WHERE Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ( N'newspaper', @Id,  N'item was updated' )
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePatent]    Script Date: 2/21/2020 11:07:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePatent]
	@Id int,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000),
	@LibraryType nvarchar(50),

	@Country nvarchar(200),
	@RegistrationNumber nvarchar(9),
	@ApplicationDate datetime,
	@PublicationDate datetime,

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		UPDATE LibraryItems
		SET 
		Title = @Title,
		PagesCount = @PagesCount,
		Commentary = @Commentary,
		LibraryType = @LibraryType
		WHERE Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		UPDATE Patents
		SET 
		Country = @Country,
		RegistrationNumber = @RegistrationNumber,
		ApplicationDate = @ApplicationDate,
		PublicationDate = @PublicationDate
		WHERE Patents.Id = @Id
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow < 1) ROLLBACK TRANSACTION
		
		IF( (SELECT dbo.IsUniquePatent(@Id)) = 0) ROLLBACK TRANSACTION
		SET @CntUpdateRow = @@ROWCOUNT
		
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'item was updated' )
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION

		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[5] 2[50] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Books"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 443
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Issues"
            Begin Extent = 
               Top = 54
               Left = 482
               Bottom = 184
               Right = 673
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Patents"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Newspapers"
            Begin Extent = 
               Top = 138
               Left = 272
               Bottom = 268
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryAuthors"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Authors"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AuthorLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AuthorLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Books"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 443
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BookLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BookLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Newspapers"
            Begin Extent = 
               Top = 6
               Left = 475
               Bottom = 210
               Right = 672
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Issues"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 208
               Right = 437
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LibraryItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 210
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IssueLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IssueLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LibraryItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patents"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatentLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatentLibraryItems'
GO
USE [master]
GO
ALTER DATABASE [Library] SET  READ_WRITE 
GO
