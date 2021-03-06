USE [master]
GO
/****** Object:  Database [Library]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE DATABASE [Library]
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
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_reader]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE LOGIN [user_reader] WITH PASSWORD=N'E+orp4tGlA6bt1DQmIeM6PPz67yBh2ErRBTu1U2TmyU=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_reader] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_librarian ]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE LOGIN [user_librarian ] WITH PASSWORD=N'Nm4RDMCWEKWU6SV1wn7qhAgEWL0I66hZIk9ApO1nnig=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_librarian ] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_admin]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE LOGIN [user_admin] WITH PASSWORD=N't4WmIFUo3ONWHA6dDrR9bmealDX6ghmqhuRensI7/i4=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_admin] DISABLE
GO
USE [Library]
GO
/****** Object:  User [user_reader]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE USER [user_reader] FOR LOGIN [user_reader] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_librarian ]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE USER [user_librarian ] FOR LOGIN [user_librarian ] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_admin]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE USER [user_admin] FOR LOGIN [user_admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [reader]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE ROLE [reader]
GO
/****** Object:  DatabaseRole [librarian ]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE ROLE [librarian ]
GO
/****** Object:  DatabaseRole [admin]    Script Date: 4/22/2020 5:31:52 PM ******/
CREATE ROLE [admin]
GO
ALTER ROLE [reader] ADD MEMBER [user_reader]
GO
ALTER ROLE [librarian ] ADD MEMBER [user_librarian ]
GO
ALTER ROLE [admin] ADD MEMBER [user_admin]
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniqueBook]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	DECLARE @Book  TABLE(Id INT, Title NVARCHAR(300), ISBN NVARCHAR(50), YearOfPublishing int)
	DECLARE @BookAuthors TABLE (AuthorId int)

	INSERT INTO  @Book (Id, Title, ISBN, YearOfPublishing)
	SELECT Id, Title, ISBN, YearOfPublishing
	FROM BookLibraryItems
	WHERE Id = @BookId

	INSERT INTO  @BookAuthors(AuthorId)
	SELECT Author_Id
	FROM LibraryAuthors 
	WHERE LibraryItem_Id = @BookId

	IF((SELECT ISBN FROM @Book) IS NOT NULL or (SELECT ISBN FROM @Book) = '')
	BEGIN
		DECLARE @ISBN nvarchar(50) = (SELECT ISBN FROM @Book)
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
	((SELECT Title FROM @Book) IS NOT NULL or (SELECT Title FROM @Book) = '')
	AND ((SELECT YearOfPublishing FROM @Book) IS NOT NULL or (SELECT YearOfPublishing FROM @Book) = 0)
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
					-- если нет разницы между авторами просто какой-то сущности в библиотеке
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
/****** Object:  UserDefinedFunction [dbo].[IsUniqueIssue]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[IsUniquePatent]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  Table [dbo].[Books]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] NOT NULL,
	[City] [nvarchar](200) NULL,
	[PublishingCompany] [nvarchar](300) NULL,
	[ISBN] [nvarchar](50) NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	[YearOfPublishing] [int] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_LibraryItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BookLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BookLibraryItems]
AS
SELECT        dbo.LibraryItems.Id, dbo.LibraryItems.Title, dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.LibraryItems.YearOfPublishing, dbo.LibraryItems.Deleted, dbo.Books.City, 
                         dbo.Books.ISBN, dbo.Books.PublishingCompany
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Books ON dbo.LibraryItems.Id = dbo.Books.Id
GO
/****** Object:  Table [dbo].[Patents]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  View [dbo].[PatentLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PatentLibraryItems]
AS
SELECT        dbo.LibraryItems.Id, dbo.LibraryItems.Title, dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.LibraryItems.YearOfPublishing, dbo.LibraryItems.Deleted, dbo.Patents.Country, 
                         dbo.Patents.RegistrationNumber, dbo.Patents.ApplicationDate, dbo.Patents.PublicationDate
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Patents ON dbo.LibraryItems.Id = dbo.Patents.Id
GO
/****** Object:  Table [dbo].[Issues]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issues](
	[Id] [int] NOT NULL,
	[Newspaper_Id] [int] NULL,
	[CountOfPublishing] [int] NULL,
	[DateOfPublishing] [datetime] NULL,
 CONSTRAINT [PK_Newspaper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Newspapers]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	[Deleted] [int] NULL,
 CONSTRAINT [PK_Issue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[IssueLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IssueLibraryItems]
AS
SELECT        dbo.LibraryItems.Id, dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.LibraryItems.Deleted, dbo.Issues.Newspaper_Id, dbo.Issues.CountOfPublishing, 
                         dbo.Issues.DateOfPublishing, dbo.Newspapers.City, dbo.Newspapers.PublishingCompany, dbo.Newspapers.ISSN, dbo.LibraryItems.Title, dbo.LibraryItems.YearOfPublishing
FROM            dbo.LibraryItems INNER JOIN
                         dbo.Issues ON dbo.LibraryItems.Id = dbo.Issues.Id INNER JOIN
                         dbo.Newspapers ON dbo.Issues.Newspaper_Id = dbo.Newspapers.Id
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](200) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[User_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LibraryRoles]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LibraryRoles]
AS
SELECT        dbo.Roles.Id, dbo.Roles.RoleName, dbo.UserRoles.User_Id
FROM            dbo.Roles INNER JOIN
                         dbo.UserRoles ON dbo.Roles.Id = dbo.UserRoles.Role_Id
GO
/****** Object:  Table [dbo].[LibraryAuthors]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  Table [dbo].[Authors]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  View [dbo].[AuthorLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AuthorLibraryItems]
AS
SELECT        dbo.LibraryAuthors.LibraryItem_Id, dbo.Authors.Firstname, dbo.Authors.Lastname, dbo.Authors.Type, dbo.Authors.Deleted, dbo.Authors.Id, dbo.LibraryAuthors.Author_Id
FROM            dbo.LibraryAuthors INNER JOIN
                         dbo.Authors ON dbo.LibraryAuthors.Author_Id = dbo.Authors.Id
GO
/****** Object:  View [dbo].[AllLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllLibraryItems]
AS
SELECT        dbo.Books.City, dbo.Books.PublishingCompany, dbo.Books.ISBN, dbo.Issues.Newspaper_Id, dbo.Issues.CountOfPublishing, dbo.Issues.DateOfPublishing, dbo.LibraryItems.Id, dbo.LibraryItems.Title, 
                         dbo.LibraryItems.PagesCount, dbo.LibraryItems.Commentary, dbo.LibraryItems.LibraryType, dbo.Patents.Country, dbo.Patents.RegistrationNumber, dbo.Patents.ApplicationDate, dbo.Patents.PublicationDate, 
                         dbo.Newspapers.ISSN, dbo.Newspapers.PublishingCompany AS NewspapersPublishingCompany, dbo.Newspapers.City AS NewspapersCity, dbo.Newspapers.Title AS NewspapersTitle, dbo.LibraryItems.YearOfPublishing, 
                         dbo.LibraryItems.Deleted
FROM            dbo.Newspapers LEFT OUTER JOIN
                         dbo.Issues ON dbo.Newspapers.Id = dbo.Issues.Newspaper_Id RIGHT OUTER JOIN
                         dbo.LibraryItems LEFT OUTER JOIN
                         dbo.Books ON dbo.LibraryItems.Id = dbo.Books.Id ON dbo.Issues.Id = dbo.LibraryItems.Id LEFT OUTER JOIN
                         dbo.Patents ON dbo.LibraryItems.Id = dbo.Patents.Id
GO
/****** Object:  Table [dbo].[AppLogs]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](300) NULL,
	[Date] [datetime] NULL,
	[Login] [nvarchar](300) NULL,
	[AppLayer] [nvarchar](300) NULL,
	[ClassName] [nvarchar](300) NULL,
	[MethodName] [nvarchar](300) NULL,
	[ControllerName] [nvarchar](300) NULL,
	[ActionName] [nvarchar](300) NULL,
	[StackTrace] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](300) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AppLogs] ON 

INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (307, N'User: "" logged in.', CAST(N'2020-04-17T21:02:05.610' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (308, N'User: "" logged in.', CAST(N'2020-04-18T06:51:55.753' AS DateTime), N'', N'PL', NULL, NULL, N'Controller', N'', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (309, N'User: "" logged in.', CAST(N'2020-04-18T06:58:44.593' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (310, N'User: "" logged in.', CAST(N'2020-04-18T07:29:22.690' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (311, N'Test', CAST(N'2020-04-19T07:26:52.857' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 83
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (312, N'Test', CAST(N'2020-04-19T07:27:06.590' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 83
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (313, N'Test', CAST(N'2020-04-19T07:39:33.823' AS DateTime), NULL, N'Logic', N'CommonLogic', N'GetLibraryItems', N'HomeController', N'Index', N'   at Epam.Task01.Library.CollectionBLL.CommonLogic.GetLibraryItems() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.CollectionBLL\CommonLogic.cs:line 29
   at Epam.Task01.Library.MVC_PL.Controllers.HomeController.GetData(Int32 page) in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.MVC_PL\Controllers\HomeController.cs:line 27
   at lambda_method(Closure , ControllerBase , Object[] )
   at System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters)
   at System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c.<BeginInvokeSynchronousActionMethod>b__9_0(IAsyncResult asyncResult, ActionInvocation innerInvokeState)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`2.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethod(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.<>c__DisplayClass11_0.<InvokeActionMethodFilterAsynchronouslyRecursive>b__0()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.<>c__DisplayClass11_2.<InvokeActionMethodFilterAsynchronouslyRecursive>b__2()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass7_0.<BeginInvokeActionMethodWithFilters>b__1(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethodWithFilters(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass3_6.<BeginInvokeAction>b__4()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass3_1.<BeginInvokeAction>b__1(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeAction(IAsyncResult asyncResult)
   at System.Web.Mvc.Controller.<>c.<BeginExecuteCore>b__152_1(IAsyncResult asyncResult, ExecuteCoreState innerState)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncVoid`1.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.Controller.EndExecuteCore(IAsyncResult asyncResult)
   at System.Web.Mvc.Controller.<>c.<BeginExecute>b__151_2(IAsyncResult asyncResult, Controller controller)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncVoid`1.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.Controller.EndExecute(IAsyncResult asyncResult)
   at System.Web.Mvc.Controller.System.Web.Mvc.Async.IAsyncController.EndExecute(IAsyncResult asyncResult)
   at System.Web.Mvc.MvcHandler.<>c.<BeginProcessRequest>b__20_1(IAsyncResult asyncResult, ProcessRequestState innerState)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncVoid`1.CallEndDelegate(IAsyncResult asyncResult)
   at System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End()
   at System.Web.Mvc.MvcHandler.EndProcessRequest(IAsyncResult asyncResult)
   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
   at System.Web.Mvc.HttpHandlerUtil.ServerExecuteHttpHandlerAsyncWrapper.<>c__DisplayClass3_0.<EndProcessRequest>b__0()
   at System.Web.Mvc.HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.<>c__DisplayClass5_0.<Wrap>b__0()
   at System.Web.Mvc.HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap[TResult](Func`1 func)
   at System.Web.Mvc.HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap(Action action)
   at System.Web.Mvc.HttpHandlerUtil.ServerExecuteHttpHandlerAsyncWrapper.EndProcessRequest(IAsyncResult result)
   at System.Web.HttpServerUtility.ExecuteInternal(IHttpHandler handler, TextWriter writer, Boolean preserveForm, Boolean setPreviousPage, VirtualPath path, VirtualPath filePath, String physPath, Exception error, String queryStringOverride)')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (314, N'User: "" logged in.', CAST(N'2020-04-19T08:17:35.707' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (315, N'User: "" logged in.', CAST(N'2020-04-19T10:11:40.713' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (316, N'User: "" logged in.', CAST(N'2020-04-19T10:12:01.597' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (317, N'User: "" logged in.', CAST(N'2020-04-19T13:29:33.290' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (318, N'User: "" logged in.', CAST(N'2020-04-19T14:43:22.063' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (319, N'User: "" logged in.', CAST(N'2020-04-19T14:43:40.083' AS DateTime), N'', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (320, N'User: "User" logged in.', CAST(N'2020-04-19T15:11:42.317' AS DateTime), N'User', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (321, N'User: "User" has registered', CAST(N'2020-04-19T15:30:05.620' AS DateTime), N'User', N'PL', NULL, NULL, N'UserController', N'SignUp', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (322, N'User: "User" logged in.', CAST(N'2020-04-19T15:36:03.610' AS DateTime), N'User', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (323, N'User: "Admin" logged in.', CAST(N'2020-04-19T16:00:11.410' AS DateTime), N'Admin', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (324, N'User: "Librarian" logged in.', CAST(N'2020-04-19T16:38:21.613' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (325, N'User: "Admin" logged in.', CAST(N'2020-04-20T05:02:25.220' AS DateTime), N'Admin', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (326, N'Unable to cast object of type ''System.DBNull'' to type ''System.String''.', CAST(N'2020-04-20T05:26:11.383' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 85
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (327, N'Unable to cast object of type ''System.DBNull'' to type ''System.String''.', CAST(N'2020-04-20T05:26:19.120' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 85
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (328, N'User: "Librarian" logged in.', CAST(N'2020-04-20T06:30:26.820' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (329, N'User: "Librarian" logged in.', CAST(N'2020-04-20T08:12:18.280' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (330, N'User: "Librarian" logged in.', CAST(N'2020-04-20T10:55:05.423' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (331, N'User: "Librarian" logged in.', CAST(N'2020-04-20T11:45:05.820' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (332, N'User: "Librarian" logged in.', CAST(N'2020-04-20T13:11:43.417' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (333, N'User: "Admin" logged in.', CAST(N'2020-04-20T13:12:14.740' AS DateTime), N'Admin', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (334, N'User: "Librarian" logged in.', CAST(N'2020-04-20T13:13:12.843' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (335, N'User: "Librarian" logged in.', CAST(N'2020-04-20T16:23:05.360' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (336, N'User: "Librarian" logged in.', CAST(N'2020-04-20T17:48:30.470' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (337, N'User: "Librarian" logged in.', CAST(N'2020-04-21T11:30:38.980' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (338, N'User: "Librarian" logged in.', CAST(N'2020-04-21T11:59:59.943' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (339, N'User: "Librarian" logged in.', CAST(N'2020-04-21T13:29:36.740' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (340, N'User: "Librarian" logged in.', CAST(N'2020-04-21T15:01:12.820' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (341, N'User: "Librarian" logged in.', CAST(N'2020-04-21T15:24:39.127' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (342, N'User: "Librarian" logged in.', CAST(N'2020-04-21T16:20:33.647' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (343, N'User: "Librarian" logged in.', CAST(N'2020-04-21T16:30:39.667' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (344, N'User: "Librarian" logged in.', CAST(N'2020-04-21T21:23:20.363' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (345, N'User: "Librarian" logged in.', CAST(N'2020-04-21T22:24:05.907' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (346, N'User: "Librarian" logged in.', CAST(N'2020-04-21T23:04:22.010' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (347, N'User: "Librarian" logged in.', CAST(N'2020-04-22T00:15:50.250' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (348, N'Specified cast is not valid.', CAST(N'2020-04-22T00:29:24.163' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 119
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (349, N'Specified cast is not valid.', CAST(N'2020-04-22T00:29:27.207' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 119
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (350, N'Specified cast is not valid.', CAST(N'2020-04-22T05:30:02.740' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 119
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (351, N'Specified cast is not valid.', CAST(N'2020-04-22T05:30:02.740' AS DateTime), NULL, N'Dal', N'<GetLibraryItems>d__3', N'MoveNext', N'HomeController', N'GetData', N'   at Epam.Task01.Library.DBDAL.CommonDBDao.<GetLibraryItems>d__3.MoveNext() in C:\Users\Daria_Iudina\source\repos\rdrunet_q4\Epam.Task 01.Library\Epam.Task01.Library.DBDAL\CommonDBDao.cs:line 119
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.OrderedEnumerable`1.<GetEnumerator>d__1.MoveNext()
   at lambda_method(Closure , OrderedEnumerable`2 , List`1 , ResolutionContext )')
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (352, N'User: "Librarian" logged in.', CAST(N'2020-04-22T06:10:31.407' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (353, N'User: "User" logged in.', CAST(N'2020-04-22T06:51:02.020' AS DateTime), N'User', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (354, N'User: "Librarian" logged in.', CAST(N'2020-04-22T06:53:02.567' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (355, N'User: "Librarian" logged in.', CAST(N'2020-04-22T07:14:05.890' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (356, N'User: "Librarian" logged in.', CAST(N'2020-04-22T07:57:30.230' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (357, N'User: "Librarian" logged in.', CAST(N'2020-04-22T08:40:18.783' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (358, N'User: "Librarian" logged in.', CAST(N'2020-04-22T10:04:33.653' AS DateTime), N'Librarian', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
SET IDENTITY_INSERT [dbo].[AppLogs] OFF
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (349, N'John', N'Snow', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (357, N'Juli', N'Shirni', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (358, N'Sdfsf', N'Ssdfsdf', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (359, N'Жуль', N'Верн', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (360, N'Оааааывы', N'Лыоволыфв', N'Author', 0)
SET IDENTITY_INSERT [dbo].[Authors] OFF
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (219, N'Саратов', N'Комсомолец', N'ISBN 0-13-041717-3')
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (220, N'Москва', N'Эксмо', N'ISBN 5-02-013850-9')
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (221, N'Волгоград', N'Комсомолец', NULL)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (222, N'Волгоград', N'Комсомолец', NULL)
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (129, 2, 1, CAST(N'2020-02-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (232, 2, 2, CAST(N'2020-04-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (235, 3, 66, CAST(N'2020-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (236, 2, 5, CAST(N'2020-04-03T00:00:00.000' AS DateTime))
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (219, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (219, 358)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (220, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (222, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (222, 357)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (228, 349)
SET IDENTITY_INSERT [dbo].[LibraryItems] ON 

INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (129, N'Мурзилка', 100, N'issue', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (130, N'Патент на полезное изобретение', 200, N'patent', N'Patent', 1998, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (140, N'Патент очень полезный', 100, N'patent', N'Patent', 2000, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (219, N'Книжечка', 200, N'нет', N'Book', 1996, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (220, N'Книжечка12', 100, N'нет', N'Book', 2000, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (221, N'Книжечка', 100, N'нет', N'Book', 1990, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (222, N'Книжечка123', 10, N'нет', N'Book', 1800, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (228, N'ПатентПатентищеПппп', 10, N'нет', N'Patent', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (232, N'Мурзилка', 30, N'нет', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (235, N'Газета', 10, N'нет', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (236, N'Мурзилка', 100, N'нет', N'Issue', 2020, 0)
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
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (155, CAST(N'2020-02-23T18:25:11.517' AS DateTime), N'Book', 72, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (158, CAST(N'2020-02-23T18:53:57.813' AS DateTime), N'Patent', 101, N'patent item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (159, CAST(N'2020-02-23T18:58:38.467' AS DateTime), N'Issue', 102, N'issue item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (160, CAST(N'2020-02-23T20:11:39.937' AS DateTime), NULL, 103, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (171, CAST(N'2020-02-28T13:34:30.297' AS DateTime), N'Book', 114, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (172, CAST(N'2020-02-28T13:34:30.317' AS DateTime), N'Book', 114, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (174, CAST(N'2020-02-28T13:35:38.563' AS DateTime), N'Book', 115, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (177, CAST(N'2020-02-28T13:43:27.073' AS DateTime), N'Book', 114, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (181, CAST(N'2020-02-28T18:04:16.370' AS DateTime), N'Book', 121, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (182, CAST(N'2020-02-28T18:04:16.387' AS DateTime), N'Book', 121, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (183, CAST(N'2020-02-28T18:08:59.540' AS DateTime), N'Book', 121, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (192, CAST(N'2020-03-10T16:47:20.250' AS DateTime), NULL, 129, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (193, CAST(N'2020-03-10T16:49:17.657' AS DateTime), NULL, 130, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (195, CAST(N'2020-03-15T09:49:19.330' AS DateTime), NULL, 132, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (196, CAST(N'2020-03-15T10:42:15.080' AS DateTime), NULL, 133, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (197, CAST(N'2020-03-15T10:49:23.767' AS DateTime), NULL, 134, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (198, CAST(N'2020-03-15T10:49:25.607' AS DateTime), NULL, 135, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (199, CAST(N'2020-03-15T10:49:26.853' AS DateTime), NULL, 136, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (200, CAST(N'2020-03-15T10:49:29.500' AS DateTime), NULL, 137, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (201, CAST(N'2020-03-15T10:58:53.530' AS DateTime), NULL, 138, N'<Unknown type> was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (202, CAST(N'2020-03-15T11:51:20.997' AS DateTime), N'Patent', 139, N'Patent was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (203, CAST(N'2020-03-15T11:52:01.790' AS DateTime), N'Patent', 140, N'Patent was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (204, CAST(N'2020-03-18T10:15:15.583' AS DateTime), N'Issue', 141, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (225, CAST(N'2020-03-26T14:10:25.527' AS DateTime), N'Book', 159, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (226, CAST(N'2020-03-26T14:16:18.923' AS DateTime), N'Book', 159, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (232, CAST(N'2020-03-26T14:50:48.663' AS DateTime), N'Book', 165, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (233, CAST(N'2020-03-26T14:50:48.680' AS DateTime), N'Book', 165, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (234, CAST(N'2020-03-27T12:54:14.257' AS DateTime), N'Book', 166, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (235, CAST(N'2020-03-27T12:54:14.277' AS DateTime), N'Book', 166, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (236, CAST(N'2020-03-27T12:55:01.120' AS DateTime), N'Book', 167, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (237, CAST(N'2020-03-27T12:55:01.140' AS DateTime), N'Book', 167, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (238, CAST(N'2020-03-27T13:00:12.253' AS DateTime), N'Book', 168, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (239, CAST(N'2020-03-27T13:00:12.273' AS DateTime), N'Book', 168, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (240, CAST(N'2020-03-27T13:06:12.673' AS DateTime), N'Book', 169, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (241, CAST(N'2020-03-27T13:06:12.693' AS DateTime), N'Book', 169, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (242, CAST(N'2020-03-27T16:34:43.057' AS DateTime), N'Book', 170, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (243, CAST(N'2020-03-27T16:34:43.073' AS DateTime), N'Book', 170, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (244, CAST(N'2020-03-27T16:37:36.737' AS DateTime), N'Book', 171, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (245, CAST(N'2020-03-27T16:37:36.757' AS DateTime), N'Book', 171, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (246, CAST(N'2020-03-27T16:50:35.133' AS DateTime), N'Book', 172, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (247, CAST(N'2020-03-27T16:50:35.150' AS DateTime), N'Book', 172, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (248, CAST(N'2020-03-27T16:56:01.743' AS DateTime), N'Book', 173, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (249, CAST(N'2020-03-27T16:56:01.763' AS DateTime), N'Book', 173, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (250, CAST(N'2020-03-27T16:56:57.677' AS DateTime), N'Book', 174, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (251, CAST(N'2020-03-27T16:56:57.697' AS DateTime), N'Book', 174, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (252, CAST(N'2020-03-27T16:58:37.317' AS DateTime), N'Book', 175, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (253, CAST(N'2020-03-27T16:58:37.333' AS DateTime), N'Book', 175, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (254, CAST(N'2020-03-27T17:11:16.140' AS DateTime), N'Book', 176, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (255, CAST(N'2020-03-27T17:11:16.157' AS DateTime), N'Book', 176, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (283, CAST(N'2020-03-29T11:57:01.007' AS DateTime), N'Book', 176, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (284, CAST(N'2020-03-29T11:58:31.147' AS DateTime), N'Book', 175, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (285, CAST(N'2020-03-29T11:59:40.987' AS DateTime), N'Book', 174, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (286, CAST(N'2020-03-29T13:21:40.413' AS DateTime), N'Book', 173, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (288, CAST(N'2020-03-29T14:52:11.970' AS DateTime), N'Book', 205, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (289, CAST(N'2020-03-29T14:52:12.000' AS DateTime), N'Book', 205, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (290, CAST(N'2020-03-29T16:55:07.423' AS DateTime), N'Book', 205, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (291, CAST(N'2020-03-29T16:55:15.050' AS DateTime), N'Book', 172, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (292, CAST(N'2020-03-29T16:55:24.177' AS DateTime), N'Book', 166, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (293, CAST(N'2020-03-29T16:55:35.220' AS DateTime), N'Book', 165, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (294, CAST(N'2020-03-29T16:55:40.770' AS DateTime), N'Book', 171, N'book item was deleted by administrator', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (295, CAST(N'2020-03-29T19:01:25.397' AS DateTime), N'Book', 206, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (296, CAST(N'2020-03-29T19:01:25.413' AS DateTime), N'Book', 206, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (297, CAST(N'2020-03-29T19:07:10.910' AS DateTime), N'Book', 207, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (298, CAST(N'2020-03-29T19:07:10.923' AS DateTime), N'Book', 207, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (299, CAST(N'2020-03-29T20:27:12.667' AS DateTime), N'Book', 208, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (300, CAST(N'2020-03-29T20:27:12.687' AS DateTime), N'Book', 208, N'item was added', N'dbo')
GO
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (301, CAST(N'2020-03-29T21:22:55.537' AS DateTime), N'User', 1, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (302, CAST(N'2020-03-29T21:44:09.520' AS DateTime), N'User', 2, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (303, CAST(N'2020-03-29T22:40:31.027' AS DateTime), N'User', 3, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (304, CAST(N'2020-03-30T16:15:25.250' AS DateTime), N'Book', 209, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (305, CAST(N'2020-03-30T16:15:25.277' AS DateTime), N'Book', 209, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (306, CAST(N'2020-03-30T16:20:26.577' AS DateTime), N'Book', 210, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (307, CAST(N'2020-03-30T16:20:26.600' AS DateTime), N'Book', 210, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (308, CAST(N'2020-03-31T14:03:26.233' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (309, CAST(N'2020-03-31T14:20:51.250' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (310, CAST(N'2020-03-31T14:40:37.677' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (311, CAST(N'2020-03-31T14:42:16.220' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (312, CAST(N'2020-03-31T14:50:18.430' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (313, CAST(N'2020-03-31T14:50:32.390' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (314, CAST(N'2020-03-31T16:18:12.090' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (316, CAST(N'2020-03-31T17:27:23.580' AS DateTime), N'User', 4, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (317, CAST(N'2020-03-31T17:28:34.843' AS DateTime), N'User', 5, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (318, CAST(N'2020-03-31T17:29:01.577' AS DateTime), N'User', 5, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (319, CAST(N'2020-03-31T23:38:05.847' AS DateTime), N'User', 3, N'user roles was changed', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (320, CAST(N'2020-04-11T11:57:22.380' AS DateTime), N'Book', 211, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (321, CAST(N'2020-04-11T11:57:22.423' AS DateTime), N'Book', 211, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (322, CAST(N'2020-04-11T12:00:21.893' AS DateTime), N'Book', 212, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (323, CAST(N'2020-04-11T12:00:21.917' AS DateTime), N'Book', 212, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (324, CAST(N'2020-04-12T19:00:09.650' AS DateTime), N'Book', 213, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (325, CAST(N'2020-04-12T19:00:09.667' AS DateTime), N'Book', 213, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (326, CAST(N'2020-04-12T19:04:10.567' AS DateTime), N'Book', 214, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (327, CAST(N'2020-04-12T19:04:10.583' AS DateTime), N'Book', 214, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (328, CAST(N'2020-04-12T19:08:45.627' AS DateTime), N'Book', 215, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (329, CAST(N'2020-04-12T19:08:45.643' AS DateTime), N'Book', 215, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (330, CAST(N'2020-04-12T19:57:05.770' AS DateTime), N'Book', 216, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (331, CAST(N'2020-04-12T19:57:05.787' AS DateTime), N'Book', 216, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (332, CAST(N'2020-04-12T20:01:57.550' AS DateTime), N'Book', 217, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (333, CAST(N'2020-04-12T20:01:57.567' AS DateTime), N'Book', 217, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (334, CAST(N'2020-04-12T20:14:06.673' AS DateTime), N'Book', 218, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (335, CAST(N'2020-04-12T20:14:06.690' AS DateTime), N'Book', 218, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (336, CAST(N'2020-04-12T21:53:11.163' AS DateTime), N'Book', 219, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (337, CAST(N'2020-04-12T21:53:11.183' AS DateTime), N'Book', 219, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (338, CAST(N'2020-04-13T14:50:39.313' AS DateTime), N'Book', 220, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (339, CAST(N'2020-04-13T14:50:39.337' AS DateTime), N'Book', 220, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (340, CAST(N'2020-04-16T22:08:52.110' AS DateTime), NULL, 350, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (341, CAST(N'2020-04-16T23:13:16.240' AS DateTime), NULL, 351, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (342, CAST(N'2020-04-16T23:41:22.843' AS DateTime), NULL, 352, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (343, CAST(N'2020-04-16T23:41:40.357' AS DateTime), NULL, 353, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (344, CAST(N'2020-04-16T23:46:00.720' AS DateTime), NULL, 354, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (345, CAST(N'2020-04-16T23:52:08.087' AS DateTime), NULL, 355, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (346, CAST(N'2020-04-17T00:19:50.950' AS DateTime), NULL, 356, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (347, CAST(N'2020-04-17T18:41:23.427' AS DateTime), N'User', 6, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (348, CAST(N'2020-04-18T00:49:32.723' AS DateTime), N'User', 7, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (349, CAST(N'2020-04-18T11:03:37.393' AS DateTime), N'User', 8, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (350, CAST(N'2020-04-18T11:04:44.437' AS DateTime), N'User', 9, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (351, CAST(N'2020-04-18T11:27:52.497' AS DateTime), N'User', 10, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (352, CAST(N'2020-04-18T11:33:32.963' AS DateTime), N'User', 11, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (353, CAST(N'2020-04-19T18:44:16.367' AS DateTime), N'User', 12, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (354, CAST(N'2020-04-19T18:51:53.410' AS DateTime), N'User', 13, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (355, CAST(N'2020-04-19T19:30:05.617' AS DateTime), N'User', 14, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (356, CAST(N'2020-04-20T09:25:50.743' AS DateTime), N'Book', 221, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (357, CAST(N'2020-04-20T09:25:50.770' AS DateTime), N'Book', 221, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (358, CAST(N'2020-04-20T10:41:15.740' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (359, CAST(N'2020-04-20T10:43:56.170' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (360, CAST(N'2020-04-20T10:45:27.020' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (361, CAST(N'2020-04-20T10:47:03.013' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (362, CAST(N'2020-04-20T10:47:40.110' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (363, CAST(N'2020-04-20T10:54:06.683' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (364, CAST(N'2020-04-20T10:59:20.473' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (365, CAST(N'2020-04-20T11:00:50.290' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (366, CAST(N'2020-04-20T11:02:53.470' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (367, CAST(N'2020-04-20T11:20:19.657' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (368, CAST(N'2020-04-20T11:20:27.890' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (369, CAST(N'2020-04-20T11:20:32.340' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (370, CAST(N'2020-04-20T11:20:52.773' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (371, CAST(N'2020-04-20T12:14:21.993' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (372, CAST(N'2020-04-20T12:15:34.210' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (373, CAST(N'2020-04-20T15:14:47.590' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (374, CAST(N'2020-04-20T15:15:01.303' AS DateTime), N'Book', 220, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (375, CAST(N'2020-04-20T16:10:53.910' AS DateTime), N'Newspaper', 3, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (376, CAST(N'2020-04-20T17:07:46.180' AS DateTime), N'Book', 222, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (377, CAST(N'2020-04-20T17:07:46.197' AS DateTime), N'Book', 222, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (378, CAST(N'2020-04-20T20:52:13.137' AS DateTime), NULL, 358, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (383, CAST(N'2020-04-21T16:07:38.120' AS DateTime), N'Patent', 227, N'Patent was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (384, CAST(N'2020-04-21T16:10:50.260' AS DateTime), N'Patent', 228, N'Patent was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (385, CAST(N'2020-04-21T16:10:50.273' AS DateTime), N'Patent', 228, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (386, CAST(N'2020-04-21T16:16:22.093' AS DateTime), N'Book', 219, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (388, CAST(N'2020-04-21T20:50:01.137' AS DateTime), N'Issue', 230, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (389, CAST(N'2020-04-21T20:50:01.153' AS DateTime), N'Issue', 230, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (390, CAST(N'2020-04-22T01:40:01.540' AS DateTime), NULL, 359, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (391, CAST(N'2020-04-22T01:40:42.663' AS DateTime), NULL, 360, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (392, CAST(N'2020-04-22T03:09:13.583' AS DateTime), N'Issue', 231, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (393, CAST(N'2020-04-22T03:09:13.600' AS DateTime), N'Issue', 231, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (394, CAST(N'2020-04-22T03:16:44.577' AS DateTime), N'Issue', 232, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (395, CAST(N'2020-04-22T03:16:44.590' AS DateTime), N'Issue', 232, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (396, CAST(N'2020-04-22T03:23:08.097' AS DateTime), N'Issue', 233, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (397, CAST(N'2020-04-22T03:23:08.113' AS DateTime), N'Issue', 233, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (398, CAST(N'2020-04-22T03:24:41.713' AS DateTime), N'Issue', 234, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (399, CAST(N'2020-04-22T03:24:41.730' AS DateTime), N'Issue', 234, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (400, CAST(N'2020-04-22T03:58:14.937' AS DateTime), N'Issue', 235, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (401, CAST(N'2020-04-22T03:58:14.953' AS DateTime), N'Issue', 235, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (402, CAST(N'2020-04-22T04:22:00.717' AS DateTime), N'Issue', 236, N'Issue was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (403, CAST(N'2020-04-22T04:22:00.733' AS DateTime), N'Issue', 236, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (404, CAST(N'2020-04-22T14:38:25.367' AS DateTime), N'Issue', 235, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (405, CAST(N'2020-04-22T14:42:00.810' AS DateTime), N'Issue', 235, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (406, CAST(N'2020-04-22T15:08:14.633' AS DateTime), N'Patent', 228, N'item was updated', N'dbo')
GO
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (407, CAST(N'2020-04-22T15:08:41.320' AS DateTime), N'Patent', 228, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (408, CAST(N'2020-04-22T15:11:21.447' AS DateTime), N'Patent', 228, N'item was updated', N'dbo')
SET IDENTITY_INSERT [dbo].[Logs] OFF
SET IDENTITY_INSERT [dbo].[Newspapers] ON 

INSERT [dbo].[Newspapers] ([Id], [Title], [City], [PublishingCompany], [ISSN], [Deleted]) VALUES (2, N'Мурзилка', N'moskow', N'newspaper', N'ISSN', 0)
INSERT [dbo].[Newspapers] ([Id], [Title], [City], [PublishingCompany], [ISSN], [Deleted]) VALUES (3, N'Газета', N'Волгоград', N'Эксмо', N'ISSN 0317-8471', 0)
SET IDENTITY_INSERT [dbo].[Newspapers] OFF
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (130, N'Russia', N' 2201527', NULL, CAST(N'1994-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (140, N'USA', N' 2201525', CAST(N'1994-02-02T00:00:00.000' AS DateTime), CAST(N'1994-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (228, N'Россияяя', N'123456789', CAST(N'2020-04-01T00:00:00.000' AS DateTime), CAST(N'2020-04-04T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Librarian')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (3, 1)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (3, 2)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (3, 3)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (4, 3)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (5, 2)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (5, 3)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (3, N'Admin', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (4, N'User', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (5, N'Librarian', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_DateAndTimeOfOperation]  DEFAULT (getdate()) FOR [Time]
GO
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_UserName]  DEFAULT (user_name()) FOR [UserName]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_LibraryItems1] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_LibraryItems1]
GO
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Newspapers_LibraryItems] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Newspapers_LibraryItems]
GO
ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_Newspapers_NewspaperIssues] FOREIGN KEY([Newspaper_Id])
REFERENCES [dbo].[Newspapers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_Newspapers_NewspaperIssues]
GO
ALTER TABLE [dbo].[LibraryAuthors]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAuthors_Authors] FOREIGN KEY([Author_Id])
REFERENCES [dbo].[Authors] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LibraryAuthors] CHECK CONSTRAINT [FK_LibraryAuthors_Authors]
GO
ALTER TABLE [dbo].[LibraryAuthors]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAuthors_LibraryItems] FOREIGN KEY([LibraryItem_Id])
REFERENCES [dbo].[LibraryItems] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LibraryAuthors] CHECK CONSTRAINT [FK_LibraryAuthors_LibraryItems]
GO
ALTER TABLE [dbo].[Patents]  WITH CHECK ADD  CONSTRAINT [FK_Patents_LibraryItems] FOREIGN KEY([Id])
REFERENCES [dbo].[LibraryItems] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patents] CHECK CONSTRAINT [FK_Patents_LibraryItems]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddAppLog]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAppLog] 
	@Id int out,
	@Message nvarchar(300),
	@MethodName nvarchar(300)= NULL,
	@ClassName nvarchar(300)= NULL,
	@Date datetime,
	@Login nvarchar(300) = NULL,
	@AppLayer nvarchar(300)= NULL,
	@ControllerName nvarchar(300) = NULL,
	@ActionName nvarchar(300) = NULL,
	@StackTrace nvarchar(MAX) = NULL
AS
BEGIN
	INSERT INTO AppLogs([Message], MethodName,ClassName, [Date], [Login], AppLayer, ControllerName, ActionName,
	StackTrace)
	VALUES (@Message, @MethodName, @ClassName, @Date, @Login, @AppLayer, @ControllerName, @ActionName,
	@StackTrace)
	SET @Id = @@Identity
END
GO
/****** Object:  StoredProcedure [dbo].[AddAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAuthor]
	@Id int output,
	@FirstName nvarchar(300), 
	@LastName nvarchar(300),
	@Type nvarchar(300)
AS
BEGIN
BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		INSERT INTO Authors(FirstName, LastName, [Type], Deleted)
		VALUES(@FirstName, @LastName, @Type, 0)
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
/****** Object:  StoredProcedure [dbo].[AddAuthors]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddBook]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBook]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000) = NULL,
	@LibraryType nvarchar(50),

	@City nvarchar(200),
	@PublishingCompany nvarchar(200),
	@ISBN nvarchar(50) = NULL,
	@YearOfPublishing int,

	@listAuthorsId nvarchar(MAX) = NULL
AS
BEGIN
	SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		EXEC AddBookInTable @Id OUTPUT, @Title, @PagesCount, @Commentary, @LibraryType, @City,
							@PublishingCompany, @ISBN, @YearOfPublishing;
		IF(@Id IS NULL or @Id = -1) RAISERROR('error', 16,1)
		IF( (SELECT dbo.IsUniqueBook(@Id)) = 0) RAISERROR('error', 16,1)
		IF(@listAuthorsId IS NOT NULL AND @listAuthorsId <> N'[]')
		BEGIN
			EXEC AddRelationshipBookAuthors @listAuthorsId , @Id; 
			IF(@Id IS NULL) RAISERROR('error', 16,1)
		END
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id ,  N'item was added')
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION
		  SET @Id = -1;
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddBookInTable]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	@PublishingCompany nvarchar(300),
	@ISBN nvarchar(50) = NULL,
	@YearOfPublishing int
AS
BEGIN
		INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, Deleted, YearOfPublishing)
		VALUES(@Title, @PagesCount, @Commentary, @LibraryType, 0, @YearOfPublishing)

		SET @Id = SCOPE_IDENTITY();
		INSERT INTO Books(Id, City, PublishingCompany, ISBN)
		VALUES (@Id, @City, @PublishingCompany, @ISBN)
END
GO
/****** Object:  StoredProcedure [dbo].[AddIssue]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	@CountOfPublishing int = NULL,
	@DateOfPublishing datetime
	
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
		DECLARE @YearOfPublishing int  = YEAR(@DateOfPublishing);
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
/****** Object:  StoredProcedure [dbo].[AddIssueInTable]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, YearOfPublishing, Deleted)
	VALUES(@Title, @PagesCount, @Commentary, @LibraryType, @YearOfPublishing, 0)
	SET @Id = SCOPE_IDENTITY();
	INSERT INTO Issues(Id, Newspaper_Id, CountOfPublishing, DateOfPublishing)
	VALUES (@Id, @Newspaper_Id, @CountOfPublishing, @DateOfPublishing)
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewspaper]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewspaper]
	@Id int output,
	@Title nvarchar(300),
	@City nvarchar(200),
	@PublishingCompany nvarchar(300),
	@ISSN nvarchar(50)
AS
BEGIN
BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		INSERT INTO Newspapers(Title, City, PublishingCompany, ISSN, Deleted)
		VALUES(@Title, @City, @PublishingCompany, @ISSN, 0)
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
/****** Object:  StoredProcedure [dbo].[AddPatent]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddPatent]
	@Id int output,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000) = NULL,
	@LibraryType nvarchar(50),

	@Country nvarchar(200),
	@RegistrationNumber nvarchar(50),
	@ApplicationDate datetime = NULL,
	@PublicationDate datetime,

	@listAuthorsId nvarchar(MAX)
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN

		EXEC AddPatentInTable @Id OUTPUT, @Title, @PagesCount, @Commentary, @LibraryType, 
							  @Country, @RegistrationNumber, @ApplicationDate, @PublicationDate;
		IF(@Id IS NULL OR @listAuthorsId IS NULL) ROLLBACK TRANSACTION

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
/****** Object:  StoredProcedure [dbo].[AddPatentInTable]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	@RegistrationNumber nvarchar(50),
	@ApplicationDate datetime,
	@PublicationDate datetime
AS
BEGIN
	INSERT INTO LibraryItems(Title, PagesCount, Commentary, LibraryType, Deleted, YearOfPublishing)
	VALUES(@Title, @PagesCount, @Commentary, @LibraryType, 0, YEAR(@PublicationDate))
	SET @Id = SCOPE_IDENTITY();
	INSERT INTO Patents( Id, Country, RegistrationNumber, ApplicationDate, PublicationDate)
	VALUES (@Id, @Country, @RegistrationNumber, @ApplicationDate, @PublicationDate)
END
GO
/****** Object:  StoredProcedure [dbo].[AddRelationshipBookAuthors]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
	@Id int output,
	@Login nvarchar(300), 
	@Password nvarchar(max)
AS
BEGIN
BEGIN TRY
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRAN
		INSERT INTO Users(Login, Password)
		VALUES(@Login, @Password)
		SET @Id = @@IDENTITY

		INSERT INTO UserRoles([User_Id], Role_Id )
		VALUES(@Id, 3)
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ('User', @Id,  N'user was added')
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
/****** Object:  StoredProcedure [dbo].[ChangeUserRoles]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeUserRoles]
	@userId int,
	@listRolesId nvarchar(MAX) = NULL
AS
BEGIN
	SET XACT_ABORT ON
	BEGIN TRY
		BEGIN TRAN
		IF(@listRolesId IS NOT NULL AND @listRolesId <> N'[]')
		BEGIN
			DELETE FROM UserRoles
			WHERE [User_Id] = @userId

			INSERT INTO UserRoles([User_Id], Role_Id)
			SELECT  @userId, Id
			FROM OPENJSON(@listRolesId)
			  WITH (
				Id int '$.Id'
			  );
		INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ('User', @userId ,  N'user roles was changed')
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
	  IF (@@TRANCOUNT > 0 )
	  BEGIN
		  ROLLBACK TRANSACTION
		  SET @userId = -1;
		  DECLARE @msg nvarchar(2048) = error_message()  
		  RAISERROR (@msg, 16, 1)
		  RETURN 55555
	  END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteAuthors]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteBook]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteIssue]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteLibraryItem]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLibraryItem]
@Id int
AS
BEGIN
SET XACT_ABORT ON
	BEGIN TRY
		BEGIN TRAN
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
/****** Object:  StoredProcedure [dbo].[DeleteLibraryItemById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteLibraryItemById]
	@Id int,
	@CntDeleteRow int output
AS
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DECLARE @libraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id)
			IF(@libraryType = 'Book')
			BEGIN
				EXEC dbo.DeleteBook @Id, @CntDeleteRow output
			END
			IF(@libraryType = 'Patent')
			BEGIN
				EXEC dbo.DeletePatent @Id, @CntDeleteRow output
			END
			IF(@libraryType = 'Issue')
			BEGIN
				EXEC dbo.DeleteIssue @Id, @CntDeleteRow output
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

GO
/****** Object:  StoredProcedure [dbo].[DeleteNewspaper]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[DeletePatent]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAuthorById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAuthorById]
	@Id int
AS
BEGIN
	SELECT Id, Firstname, Lastname
	FROM Authors
	WHERE Id = @Id AND Deleted <>1
END
GO
/****** Object:  StoredProcedure [dbo].[GetAuthors]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAuthors]
AS
BEGIN
	SELECT Id, Firstname, Lastname
	FROM Authors
	WHERE Deleted <> 1
	ORDER BY Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetBookById]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	WHERE t.Id = @Id AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooks]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBooks] 
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN, YearOfPublishing, 
		Deleted,
		( SELECT Author_Id as Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM BookLibraryItems as t 
	WHERE Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooksAndPatentsByAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBooksAndPatentsByAuthor]
	@AuthorId int
AS
BEGIN
	EXEC GetBooksByAuthor @AuthorId
	EXEC GetPatentByAuthor @AuthorId
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
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
			WHERE Id = @count AND Deleted <> 1
			SET @count = @count + 1
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByPublishingCompanyStartsWithInputText]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	WHERE PublishingCompany LIKE ''+ @PublishingCompany + '%' AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetIssueById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetIssueById]
	@Id int
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
	WHERE Id = @Id AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetIssues]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetIssues]
AS
BEGIN
    SELECT Id, PagesCount, Commentary, 
	       CountOfPublishing, DateOfPublishing, YearOfPublishing, Deleted, 
		   (SELECT TOP(1) Id, Title, City, PublishingCompany, ISSN as Issn
		   FROM Newspapers 
		   WHERE Id = issue.Newspaper_Id
		   FOR JSON PATH, INCLUDE_NULL_VALUES) 
		   as Newspaper
    FROM IssueLibraryItems issue 
	WHERE Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetIssuesByNewspaperId]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetIssuesByNewspaperId] 
	@newspaperId int,
	@currentIssueId int
AS
BEGIN
	SELECT Id, PagesCount, Commentary, 
	    CountOfPublishing, DateOfPublishing, YearOfPublishing, 
		(SELECT TOP(1) Id, Title, City, PublishingCompany, ISSN as Issn
		FROM Newspapers 
		WHERE Id = issue.Newspaper_Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) 
		as Newspaper
    FROM IssueLibraryItems issue 
	WHERE Deleted <> 1 and Newspaper_Id = @newspaperId
		and Id <> @currentIssueId
END
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItemById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLibraryItemById]
	 @Id int
AS
BEGIN
		SELECT TOP(1) Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN,Newspaper_Id, CountOfPublishing,
		DateOfPublishing, Title, LibraryType, Country, RegistrationNumber, ApplicationDate, PublicationDate,
		ISSN, NewspapersPublishingCompany, NewspapersCity, NewspapersTitle, YearOfPublishing,
		( SELECT Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors,
		(SELECT Id, Title, City, PublishingCompany, ISSN as Issn
		FROM Newspapers 
		WHERE Id = Newspaper_Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) 
		as Newspaper
    FROM AllLibraryItems as t WHERE Deleted <> 1 and Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItems]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLibraryItems]
AS
BEGIN
	SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN,Newspaper_Id, CountOfPublishing,
	DateOfPublishing, Title, LibraryType, Country, RegistrationNumber, ApplicationDate, PublicationDate,
	ISSN, NewspapersPublishingCompany, NewspapersCity, NewspapersTitle, YearOfPublishing,
	( SELECT Id, Firstname, Lastname
	FROM AuthorLibraryItems 
	WHERE LibraryItem_Id = t.Id
	FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors,
	(SELECT Id, Title, City, PublishingCompany, ISSN as Issn
	FROM Newspapers 
	WHERE Id = Newspaper_Id
	FOR JSON PATH, INCLUDE_NULL_VALUES) 
	as Newspaper
	FROM AllLibraryItems as t WHERE Deleted <> 1 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItemsByTitle]    Script Date: 4/22/2020 5:31:52 PM ******/
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
	WHERE Title LIKE @Title +'%' AND Deleted <> 1


	SELECT Id, Title, PagesCount, Commentary, 
		Country, ApplicationDate, PublicationDate, RegistrationNumber, 
				( SELECT Author_Id, Firstname, Lastname
				FROM AuthorLibraryItems 
				WHERE LibraryItem_Id = t.Id 
				FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM PatentLibraryItems as t 
	WHERE Title LIKE @Title +'%' AND Deleted <> 1

    SELECT Id, PagesCount, Commentary, 
	       CountOfPublishing, DateOfPublishing, YearOfPublishing, 
		   (SELECT TOP(1)Id, Title, City, PublishingCompany, ISSN as Issn
		   FROM Newspapers 
		   WHERE Id = issue.Newspaper_Id
		   FOR JSON PATH, INCLUDE_NULL_VALUES) 
		   as Newspaper
    FROM IssueLibraryItems issue
	WHERE Title LIKE @Title +'%' AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewspaperById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNewspaperById]
  @Id int
AS
BEGIN
	SELECT Id, Title, City, PublishingCompany, ISSN
	FROM Newspapers 
	WHERE Id =  @Id and Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapers]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNewspapers]
AS
BEGIN
	SELECT Id, Title, City, PublishingCompany, ISSN
	FROM Newspapers 
	WHERE Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapersById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNewspapersById] 
	@Id int 
AS
BEGIN
	SELECT Id, Title, City, PublishingCompany, ISSN
	FROM Newspapers
	WHERE Id = @Id AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetPatentByAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetPatentById]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatentById]
	@Id int
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, 
		   Country, ApplicationDate, PublicationDate, RegistrationNumber, 
					( SELECT Author_Id, Firstname, Lastname
					FROM AuthorLibraryItems 
					WHERE LibraryItem_Id = t.Id 
					FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM PatentLibraryItems as t
	WHERE Id = @Id AND Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetPatents]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPatents]
AS
BEGIN
    SELECT Id, Title, PagesCount, Commentary, 
		   Country, ApplicationDate, PublicationDate, RegistrationNumber, Deleted, 
					( SELECT Author_Id, Firstname, Lastname
					FROM AuthorLibraryItems 
					WHERE LibraryItem_Id = t.Id 
					FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors
    FROM PatentLibraryItems as t
	WHERE Deleted <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRoles]

AS
BEGIN
	SELECT Id, RoleName  FROM Roles
END
GO
/****** Object:  StoredProcedure [dbo].[GetSortedLibraryItemsByYear]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSortedLibraryItemsByYear]
AS
BEGIN
	SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN,Newspaper_Id, CountOfPublishing,
		DateOfPublishing, Title, LibraryType, Country, RegistrationNumber, ApplicationDate, PublicationDate,
		ISSN, NewspapersPublishingCompany, NewspapersCity, NewspapersTitle, YearOfPublishing,
		( SELECT Author_Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors,
		(SELECT Id, Title, City, PublishingCompany, ISSN as Issn
		FROM Newspapers 
		WHERE Id = Newspaper_Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) 
		as Newspaper
    FROM AllLibraryItems as t WHERE Deleted <> 1
	ORDER BY YearOfPublishing
END
GO
/****** Object:  StoredProcedure [dbo].[GetSortedLibraryItemsByYearDesc]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSortedLibraryItemsByYearDesc]
AS
BEGIN
	SELECT Id, Title, PagesCount, Commentary, City, PublishingCompany, ISBN,Newspaper_Id, CountOfPublishing,
		DateOfPublishing, Title, LibraryType, Country, RegistrationNumber, ApplicationDate, PublicationDate,
		ISSN, NewspapersPublishingCompany, NewspapersCity, NewspapersTitle, YearOfPublishing,
		( SELECT Author_Id, Firstname, Lastname
		FROM AuthorLibraryItems 
		WHERE LibraryItem_Id = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Authors,
		(SELECT Id, Title, City, PublishingCompany, ISSN as Issn
		FROM Newspapers 
		WHERE Id = Newspaper_Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) 
		as Newspaper
    FROM AllLibraryItems as t WHERE Deleted <> 1
	ORDER BY YearOfPublishing DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
    SELECT Id, Login, Password,
		( SELECT Id, RoleName
		FROM LibraryRoles 
		WHERE [User_Id] = t.Id
		FOR JSON PATH, INCLUDE_NULL_VALUES) as Roles
    FROM Users as t 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAuthor]    Script Date: 4/22/2020 5:31:52 PM ******/
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
		WHERE Id = @Id AND Deleted <> 1
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
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 4/22/2020 5:31:52 PM ******/
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
		LibraryType = @LibraryType,
		YearOfPublishing = @YearOfPublishing
		WHERE Id = @Id AND Deleted <> 1
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		UPDATE Books
		SET 
		City = @City,
		PublishingCompany = @PublishingCompany,
		ISBN = @ISBN
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
/****** Object:  StoredProcedure [dbo].[UpdateIssue]    Script Date: 4/22/2020 5:31:52 PM ******/
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

	@CntUpdateRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		DECLARE @YearOfPublishing int  = YEAR(@DateOfPublishing);
		BEGIN TRAN
		UPDATE LibraryItems
		SET 
		Title = @Title,
		PagesCount = @PagesCount,
		Commentary = @Commentary,
		LibraryType = @LibraryType,
		YearOfPublishing = @YearOfPublishing
		WHERE Id = @Id AND Deleted <> 1
		SET @CntUpdateRow = @@ROWCOUNT
		IF(@CntUpdateRow <> 1) ROLLBACK TRANSACTION

		UPDATE Issues
		SET 
		Newspaper_Id = @Newspaper_Id,
		CountOfPublishing = @CountOfPublishing,
		DateOfPublishing = @DateOfPublishing
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
/****** Object:  StoredProcedure [dbo].[UpdateNewspaper]    Script Date: 4/22/2020 5:31:52 PM ******/
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
		ISSN = @ISSN
		WHERE Id = @Id AND Deleted <> 1
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
/****** Object:  StoredProcedure [dbo].[UpdatePatent]    Script Date: 4/22/2020 5:31:52 PM ******/
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
		LibraryType = @LibraryType,
		YearOfPublishing = YEAR(@PublicationDate)
		WHERE Id = @Id AND Deleted <> 1
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
		
		DELETE FROM LibraryAuthors
		WHERE LibraryItem_Id = @id
		
		IF(@AuthorsId IS NOT NULL)
		BEGIN
			EXEC AddRelationshipBookAuthors @AuthorsId , @Id; 
			IF(@Id IS NULL) ROLLBACK TRANSACTION
		END

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
/****** Object:  Trigger [dbo].[LibraryItemAddingLogging]    Script Date: 4/22/2020 5:31:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[LibraryItemAddingLogging]
ON [dbo].[LibraryItems]
AFTER INSERT
AS
BEGIN
	DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM inserted)
	DECLARE @Id int = (SELECT Id FROM inserted)
	INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType , @Id, N'' + ISNULL(@LibraryType + N' was added', N'<Unknown type>' + N' was added'))
END
GO
ALTER TABLE [dbo].[LibraryItems] ENABLE TRIGGER [LibraryItemAddingLogging]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[20] 2[36] 3) )"
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
               Top = 138
               Left = 272
               Bottom = 268
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 2
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
         Begin Table = "LibraryItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 3
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
         Or =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AllLibraryItems'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1350
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
               Bottom = 182
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Authors"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 181
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
               Bottom = 210
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
         Begin Table = "Roles"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 182
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserRoles"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 187
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LibraryRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LibraryRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[23] 2[30] 3) )"
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
               Bottom = 204
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patents"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 185
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
