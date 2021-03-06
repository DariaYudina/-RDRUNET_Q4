USE [master]
GO
/****** Object:  Database [Library]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE DATABASE [Library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Library.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Library_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Library_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
EXEC sys.sp_db_vardecimal_storage_format N'Library', N'ON'
GO
ALTER DATABASE [Library] SET QUERY_STORE = OFF
GO
USE [Library]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_reader]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [user_reader] WITH PASSWORD=N'LVC63xbyjfYwGmzyoxffXDbX1rtMeKVV8iPxHx7uYRY=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_reader] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_librarian ]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [user_librarian ] WITH PASSWORD=N'w1fNmcMJTWerJlzcK2oUodgQLqRHKubuOM0lNmck24I=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_librarian ] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user_admin]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [user_admin] WITH PASSWORD=N't8i2PANKZq5f1bPid+iPvHjuBmx6E2S0VCJjuclnpt0=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user_admin] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [user]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [user] WITH PASSWORD=N'VZjckjHSKI8Pctm5CceQbM1m0LhAAUzXiN3R5cKRqEY=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [user] DISABLE
GO
/****** Object:  Login [SARATOV\Denis_Zhukov]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [SARATOV\Denis_Zhukov] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLTELEMETRY]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT SERVICE\SQLTELEMETRY] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLSERVERAGENT]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT SERVICE\SQLSERVERAGENT] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQLSERVER]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT Service\MSSQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [librarian]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [librarian] WITH PASSWORD=N'3Y9zbNz2c+LYf4NI/82htS3NM7J7ZKWCex17e4rjrjA=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [librarian] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [admin]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [admin] WITH PASSWORD=N'3O9rtnnDCklc/wE8vJmtyEmYCIfH1anmy6eG7Usu7SU=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER LOGIN [admin] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'O+yvZqG6KcLabEXLerJPCZ7GxKFt5gO3FhG2EGteSjI=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'JmC37KixPJeolGlZ66mjm/+OcIETNn8M901/YuIKbtc=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER AUTHORIZATION ON DATABASE::[Library] TO [SARATOV\Denis_Zhukov]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [SARATOV\Denis_Zhukov]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLSERVERAGENT]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQLSERVER]
GO
USE [Library]
GO
/****** Object:  User [user_reader]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE USER [user_reader] FOR LOGIN [user_reader] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_librarian ]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE USER [user_librarian ] FOR LOGIN [user_librarian ] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [user_admin]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE USER [user_admin] FOR LOGIN [user_admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [reader]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE ROLE [reader]
GO
/****** Object:  DatabaseRole [librarian ]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE ROLE [librarian ]
GO
/****** Object:  DatabaseRole [admin]    Script Date: 5/9/2020 7:31:48 PM ******/
CREATE ROLE [admin]
GO
ALTER AUTHORIZATION ON ROLE::[reader] TO [dbo]
GO
ALTER AUTHORIZATION ON ROLE::[librarian ] TO [dbo]
GO
ALTER AUTHORIZATION ON ROLE::[admin] TO [dbo]
GO
ALTER ROLE [reader] ADD MEMBER [user_reader]
GO
ALTER ROLE [librarian ] ADD MEMBER [user_librarian ]
GO
ALTER ROLE [admin] ADD MEMBER [user_admin]
GO
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [public] AS [dbo]
GO
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [public] AS [dbo]
GO
GRANT CONNECT TO [user_admin] AS [dbo]
GO
GRANT CONNECT TO [user_librarian ] AS [dbo]
GO
GRANT CONNECT TO [user_reader] AS [dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniqueBook]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[IsUniqueBook] TO  SCHEMA OWNER 
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniqueIssue]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[IsUniqueIssue] TO  SCHEMA OWNER 
GO
/****** Object:  UserDefinedFunction [dbo].[IsUniquePatent]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[IsUniquePatent] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Books]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Books] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[LibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[LibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[BookLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[BookLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Patents]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Patents] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[PatentLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[PatentLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Issues]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Issues] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Newspapers]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Newspapers] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[IssueLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[IssueLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Roles] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[UserRoles] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[LibraryRoles]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[LibraryRoles] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[LibraryAuthors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[LibraryAuthors] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Authors] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[AuthorLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AuthorLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[AllLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AllLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[AppLogs]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AppLogs] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Logs] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[Users] TO  SCHEMA OWNER 
GO
SET IDENTITY_INSERT [dbo].[AppLogs] ON 

INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (369, N'User: "Admin" logged in.', CAST(N'2020-05-03T13:32:43.807' AS DateTime), N'Admin', N'PL', NULL, NULL, N'UserController', N'LogIn', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (370, N'User: "user" logged in', CAST(N'2020-05-07T16:38:41.087' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (371, N'User: "user" logged in', CAST(N'2020-05-07T16:38:47.533' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (372, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-07T17:06:35.253' AS DateTime), N'user', N'PL', NULL, NULL, N'Issue', N'Get', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (373, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-07T17:08:31.043' AS DateTime), N'', N'PL', NULL, NULL, N'LibraryItems', N'Get', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (374, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:34:55.940' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (375, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:34:55.940' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (376, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:28.437' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (377, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:30.527' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (378, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:32.530' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (379, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:33.107' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (380, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:33.417' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (381, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:33.613' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (382, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:33.810' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (383, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:33.970' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (384, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:34.170' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (385, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:34.360' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (386, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:41.850' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (387, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:35:42.460' AS DateTime), N'', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (388, N'User: "user" logged in', CAST(N'2020-05-08T17:38:33.563' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (389, N'User: "user" logged in', CAST(N'2020-05-08T17:38:33.567' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (390, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:38:45.293' AS DateTime), N'user', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (391, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:38:53.547' AS DateTime), N'user', N'PL', NULL, NULL, N'Author', N'GetAuthor', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (392, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:06.697' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (393, N'User: "user" logged in', CAST(N'2020-05-08T17:39:16.863' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (394, N'User: "user" logged in', CAST(N'2020-05-08T17:39:16.863' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (395, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:26.523' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (396, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:29.037' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (397, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:29.853' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (398, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.020' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (399, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.197' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (400, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.363' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (401, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.523' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (402, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.687' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (403, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:39:30.860' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (404, N'User: "user" logged in', CAST(N'2020-05-08T17:48:55.417' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (405, N'User: "user" logged in', CAST(N'2020-05-08T17:48:55.420' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (406, N'User: "user" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T17:49:01.953' AS DateTime), N'user', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (407, N'User: "user" logged in', CAST(N'2020-05-08T17:51:05.400' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (408, N'User: "user" logged in', CAST(N'2020-05-08T17:51:05.403' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (409, N'User: "user" logged in', CAST(N'2020-05-08T18:03:28.613' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (410, N'User: "user" logged in', CAST(N'2020-05-08T18:03:28.617' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (411, N'User: "user" logged in', CAST(N'2020-05-08T18:04:23.730' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (412, N'User: "user" logged in', CAST(N'2020-05-08T18:04:23.737' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (413, N'User: "User" logged in', CAST(N'2020-05-08T18:07:50.523' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (414, N'User: "User" logged in', CAST(N'2020-05-08T18:07:50.530' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (415, N'User: "User" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-08T18:08:18.477' AS DateTime), N'User', N'PL', NULL, NULL, N'Book', N'GetBooks', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (416, N'User: "" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-09T14:11:06.990' AS DateTime), N'', N'PL', NULL, NULL, N'LibraryItems', N'Get', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (417, N'User: "user" logged in', CAST(N'2020-05-09T14:11:25.417' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (418, N'User: "user" logged in', CAST(N'2020-05-09T14:11:25.420' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (419, N'User: "User" logged in', CAST(N'2020-05-09T14:24:36.853' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (420, N'User: "User" logged in', CAST(N'2020-05-09T14:24:36.857' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (421, N'User: "User" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-09T14:24:50.347' AS DateTime), N'User', N'PL', NULL, NULL, N'LibraryItems', N'Get', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (422, N'User: "User" logged in', CAST(N'2020-05-09T14:25:00.027' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (423, N'User: "User" logged in', CAST(N'2020-05-09T14:25:00.033' AS DateTime), N'User', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (424, N'User: "User" was attempted unauthorized access to closed functionality.', CAST(N'2020-05-09T14:25:08.887' AS DateTime), N'User', N'PL', NULL, NULL, N'LibraryItems', N'Get', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (425, N'User: "user" logged in', CAST(N'2020-05-09T14:25:16.100' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (426, N'User: "user" logged in', CAST(N'2020-05-09T14:25:16.103' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (427, N'User: "user" logged in', CAST(N'2020-05-09T14:25:30.557' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (428, N'User: "user" logged in', CAST(N'2020-05-09T14:25:30.557' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (429, N'User: "user" logged in', CAST(N'2020-05-09T14:34:56.160' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (430, N'User: "user" logged in', CAST(N'2020-05-09T14:34:56.163' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (431, N'User: "user" logged in', CAST(N'2020-05-09T14:39:51.667' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (432, N'User: "user" logged in', CAST(N'2020-05-09T14:39:51.670' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (433, N'User: "user" logged in', CAST(N'2020-05-09T14:43:29.963' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (434, N'User: "user" logged in', CAST(N'2020-05-09T14:43:29.970' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (435, N'User: "user" logged in', CAST(N'2020-05-09T14:51:45.847' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (436, N'User: "user" logged in', CAST(N'2020-05-09T14:51:45.850' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (437, N'User: "user" logged in', CAST(N'2020-05-09T14:57:29.900' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (438, N'User: "user" logged in', CAST(N'2020-05-09T14:57:29.903' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (439, N'User: "user" logged in', CAST(N'2020-05-09T15:04:14.293' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (440, N'User: "user" logged in', CAST(N'2020-05-09T15:04:14.300' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (441, N'User: "user" logged in', CAST(N'2020-05-09T15:06:39.537' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (442, N'User: "user" logged in', CAST(N'2020-05-09T15:06:39.540' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (443, N'User: "user" logged in', CAST(N'2020-05-09T15:08:26.693' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (444, N'User: "user" logged in', CAST(N'2020-05-09T15:08:26.700' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (445, N'User: "user" logged in', CAST(N'2020-05-09T15:18:53.713' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (446, N'User: "user" logged in', CAST(N'2020-05-09T15:18:53.720' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (447, N'User: "user" logged in', CAST(N'2020-05-09T15:24:10.323' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (448, N'User: "user" logged in', CAST(N'2020-05-09T15:24:10.327' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (449, N'User: "user" logged in', CAST(N'2020-05-09T15:25:44.117' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
INSERT [dbo].[AppLogs] ([Id], [Message], [Date], [Login], [AppLayer], [ClassName], [MethodName], [ControllerName], [ActionName], [StackTrace]) VALUES (450, N'User: "user" logged in', CAST(N'2020-05-09T15:25:44.120' AS DateTime), N'user', N'PL', NULL, NULL, N'Account', N'Login', NULL)
SET IDENTITY_INSERT [dbo].[AppLogs] OFF
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (349, N'Иван', N'Иванов', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (357, N'Juli', N'Shirni', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (358, N'Sdfsf', N'Ssdfsdf', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (359, N'Жуль', N'Верн', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (360, N'Оааааывы', N'Лыоволыфв', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (361, N'Иван', N'Иванов', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (362, N'Петя', N'Петров', N'Author', 0)
INSERT [dbo].[Authors] ([Id], [Firstname], [Lastname], [Type], [Deleted]) VALUES (363, N'Петя', N'Петров', N'Author', 0)
SET IDENTITY_INSERT [dbo].[Authors] OFF
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (219, N'Москва', N'Издательство', NULL)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (220, N'Москва', N'Эксмо', N'ISBN 5-02-013850-9')
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (221, N'Волгоград', N'Комсомолец', NULL)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (222, N'Волгоград', N'Комсомолец', NULL)
INSERT [dbo].[Books] ([Id], [City], [PublishingCompany], [ISBN]) VALUES (237, N'Саратов', N'Слово', NULL)
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (129, 2, 1, CAST(N'2020-02-10T13:54:10.510' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (232, 2, 2, CAST(N'2020-04-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (235, 3, 66, CAST(N'2020-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Issues] ([Id], [Newspaper_Id], [CountOfPublishing], [DateOfPublishing]) VALUES (236, 2, 5, CAST(N'2020-04-03T00:00:00.000' AS DateTime))
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (130, 361)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (130, 362)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (219, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (219, 357)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (220, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (222, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (222, 357)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (228, 349)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (237, 357)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (237, 358)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (238, 359)
INSERT [dbo].[LibraryAuthors] ([LibraryItem_Id], [Author_Id]) VALUES (238, 360)
SET IDENTITY_INSERT [dbo].[LibraryItems] ON 

INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (129, N'Мурзилка', 0, N'Описание мурзилки', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (130, N'Измененный патент', 15, N'Изм патент', N'Patent', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (140, N'Патент очень полезный', 100, N'patent', N'Patent', 2000, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (219, N'Обновленная книга', 190, N'описание обновленной книги', N'Book', 1950, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (220, N'Книжечка12', 100, N'нет', N'Book', 2000, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (221, N'Книжечка', 100, N'нет', N'Book', 1990, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (222, N'Книжечка123', 10, N'нет', N'Book', 1800, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (228, N'ПатентПатентищеПппп', 10, N'нет', N'Patent', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (232, N'Мурзилка', 30, N'нет', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (235, N'Газета', 10, N'нет', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (236, N'Мурзилка', 100, N'нет', N'Issue', 2020, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (237, N'Тестовая книга', 34, N'string', N'Book', 1965, 0)
INSERT [dbo].[LibraryItems] ([Id], [Title], [PagesCount], [Commentary], [LibraryType], [YearOfPublishing], [Deleted]) VALUES (238, N'Новый патент', 15, N'описание нового патента', N'Patent', 2020, 0)
SET IDENTITY_INSERT [dbo].[LibraryItems] OFF
SET IDENTITY_INSERT [dbo].[Logs] ON 

INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (408, CAST(N'2020-04-22T15:11:21.447' AS DateTime), N'Patent', 228, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (409, CAST(N'2020-04-29T13:49:22.920' AS DateTime), NULL, 361, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (410, CAST(N'2020-04-29T14:54:28.290' AS DateTime), N'Book', 237, N'Book was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (411, CAST(N'2020-04-29T14:54:28.323' AS DateTime), N'Book', 237, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (412, CAST(N'2020-04-29T18:02:36.077' AS DateTime), N'Patent', 238, N'Patent was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (413, CAST(N'2020-04-29T18:02:36.093' AS DateTime), N'Patent', 238, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (414, CAST(N'2020-04-30T12:38:01.677' AS DateTime), NULL, 362, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (415, CAST(N'2020-04-30T12:38:51.037' AS DateTime), NULL, 363, N'item was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (416, CAST(N'2020-04-30T13:00:42.480' AS DateTime), N'author', 349, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (417, CAST(N'2020-04-30T13:44:53.053' AS DateTime), N'Book', 219, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (418, CAST(N'2020-04-30T16:50:34.823' AS DateTime), N'Book', 219, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (419, CAST(N'2020-04-30T16:51:54.587' AS DateTime), N'Book', 219, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (420, CAST(N'2020-04-30T16:58:29.687' AS DateTime), N'Book', 219, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (421, CAST(N'2020-04-30T17:10:04.660' AS DateTime), N'Patent', 130, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (422, CAST(N'2020-04-30T17:43:54.007' AS DateTime), N'User', 6, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (423, CAST(N'2020-04-30T17:49:09.470' AS DateTime), N'User', 7, N'user was added', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (424, CAST(N'2020-04-30T17:57:24.783' AS DateTime), N'Issue', 129, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (425, CAST(N'2020-04-30T18:23:42.787' AS DateTime), N'newspaper', NULL, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (426, CAST(N'2020-04-30T18:25:00.920' AS DateTime), N'newspaper', NULL, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (427, CAST(N'2020-04-30T18:26:56.003' AS DateTime), N'newspaper', NULL, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (428, CAST(N'2020-04-30T18:29:20.810' AS DateTime), N'newspaper', NULL, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (429, CAST(N'2020-04-30T18:36:45.507' AS DateTime), N'newspaper', 2, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (430, CAST(N'2020-04-30T18:37:55.213' AS DateTime), N'newspaper', 2, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (431, CAST(N'2020-04-30T18:40:32.253' AS DateTime), N'newspaper', NULL, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (432, CAST(N'2020-04-30T18:52:28.120' AS DateTime), N'newspaper', 2, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (433, CAST(N'2020-04-30T18:55:00.507' AS DateTime), N'newspaper', 2, N'item was updated', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (434, CAST(N'2020-05-01T12:50:32.820' AS DateTime), N'Book', 219, N'Attempt to delete an book item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (435, CAST(N'2020-05-01T13:02:31.670' AS DateTime), N'Patent', 130, N'Attempt to delete an book item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (438, CAST(N'2020-05-01T14:18:06.153' AS DateTime), N'Newspaper', 2, N'Attempt to delete an newspaper item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (439, CAST(N'2020-05-01T14:18:07.060' AS DateTime), N'Issue', 129, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (440, CAST(N'2020-05-01T14:18:07.060' AS DateTime), N'Issue', 232, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (441, CAST(N'2020-05-01T14:18:07.060' AS DateTime), N'Issue', 236, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (442, CAST(N'2020-05-01T14:26:01.553' AS DateTime), N'Newspaper', 2, N'Attempt to delete an newspaper item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (443, CAST(N'2020-05-01T14:26:01.553' AS DateTime), N'Issue', 129, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (444, CAST(N'2020-05-01T14:26:01.553' AS DateTime), N'Issue', 232, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (445, CAST(N'2020-05-01T14:26:01.553' AS DateTime), N'Issue', 236, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (446, CAST(N'2020-05-01T17:11:28.513' AS DateTime), N'Issue', 235, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (447, CAST(N'2020-05-01T17:12:10.480' AS DateTime), N'Newspaper', 3, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (448, CAST(N'2020-05-01T17:13:22.040' AS DateTime), N'Issue', 129, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (449, CAST(N'2020-05-01T17:13:22.040' AS DateTime), N'Newspaper', 2, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (451, CAST(N'2020-05-01T17:24:56.617' AS DateTime), N'Issue', 129, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (452, CAST(N'2020-05-01T17:29:16.420' AS DateTime), N'Issue', 235, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (453, CAST(N'2020-05-01T17:29:27.290' AS DateTime), N'Newspaper', 3, N'Attempt to delete an newspaper item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (454, CAST(N'2020-05-01T17:39:06.890' AS DateTime), N'Issue', 129, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (455, CAST(N'2020-05-01T17:39:53.327' AS DateTime), N'Issue', 232, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (456, CAST(N'2020-05-01T17:40:12.047' AS DateTime), N'Issue', 236, N'Attempt to delete an issue item by external', N'dbo')
INSERT [dbo].[Logs] ([Id], [Time], [ObjectType], [ObjectId], [Desciption], [UserName]) VALUES (457, CAST(N'2020-05-01T17:40:12.047' AS DateTime), N'Newspaper', 2, N'Attempt to delete an newspaper item by external', N'dbo')
SET IDENTITY_INSERT [dbo].[Logs] OFF
SET IDENTITY_INSERT [dbo].[Newspapers] ON 

INSERT [dbo].[Newspapers] ([Id], [Title], [City], [PublishingCompany], [ISSN], [Deleted]) VALUES (2, N'Обновленная мурзилка', N'Samara', N'new newspaper', N'ISSN 0212-456', 0)
INSERT [dbo].[Newspapers] ([Id], [Title], [City], [PublishingCompany], [ISSN], [Deleted]) VALUES (3, N'Газета', N'Волгоград', N'Эксмо', N'ISSN 0317-8471', 0)
SET IDENTITY_INSERT [dbo].[Newspapers] OFF
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (130, N'Россия', N'1', CAST(N'2020-04-28T13:08:12.180' AS DateTime), CAST(N'2020-04-30T13:08:12.180' AS DateTime))
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (140, N'USA', N' 2201525', CAST(N'1994-02-02T00:00:00.000' AS DateTime), CAST(N'1994-02-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (228, N'Россияяя', N'123456789', CAST(N'2020-04-01T00:00:00.000' AS DateTime), CAST(N'2020-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Patents] ([Id], [Country], [RegistrationNumber], [ApplicationDate], [PublicationDate]) VALUES (238, N'Россия', N'1', CAST(N'2020-04-26T13:58:56.177' AS DateTime), CAST(N'2020-04-29T13:58:56.177' AS DateTime))
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Librarian')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (3, N'User')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (4, N'External')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (4, 1)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (5, 2)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (5, 3)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (6, 3)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (6, 4)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (7, 1)
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (7, 3)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (4, N'User', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (5, N'Librarian', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (6, N'user', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
INSERT [dbo].[Users] ([Id], [Login], [Password]) VALUES (7, N'Admin', N'3C-99-09-AF-EC-25-35-4D-55-1D-AE-21-59-0B-B2-6E-38-D5-3F-21-73-B8-D3-DC-3E-EE-4C-04-7E-7A-B1-C1-EB-8B-85-10-3E-3B-E7-BA-61-3B-31-BB-5C-9C-36-21-4D-C9-F1-4A-42-FD-7A-2F-DB-84-85-6B-CA-5C-44-C2')
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
/****** Object:  StoredProcedure [dbo].[AddAppLog]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddAppLog] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddAuthors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddAuthors] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddBook]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddBook] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddBookInTable]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddBookInTable] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddIssue]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddIssue] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddIssueInTable]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddIssueInTable] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddNewspaper]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddNewspaper] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddPatent]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddPatent] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddPatentInTable]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddPatentInTable] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddRelationshipBookAuthors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddRelationshipBookAuthors] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[AddUser] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[ChangeUserRoles]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[ChangeUserRoles] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteAuthors] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteBook]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteBook] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteIssue]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteIssue] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteLibraryItem]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteLibraryItem] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteLibraryItemById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteLibraryItemById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeleteNewspaper]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeleteNewspaper] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[DeletePatent]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[DeletePatent] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetAllLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetAllLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetAuthorById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetAuthors]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetAuthors] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorsByString]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAuthorsByString]
	@Search nvarchar(251)
AS
BEGIN
	SELECT Id, Firstname, Lastname
	FROM Authors
	WHERE UPPER(Firstname + ' ' + Lastname) Like UPPER('%'+ @Search + '%') OR  UPPER(Lastname + ' ' + Firstname) Like UPPER('%'+ @Search + '%') AND Deleted <> 1
END

GO
ALTER AUTHORIZATION ON [dbo].[GetAuthorsByString] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetBookById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetBookById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetBooks]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetBooks] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetBooksAndPatentsByAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetBooksAndPatentsByAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetBooksByAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetBooksByPublishingCompanyStartsWithInputText]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetBooksByPublishingCompanyStartsWithInputText] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetIssueById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetIssueById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetIssues]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetIssues] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetIssuesByNewspaperId]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetIssuesByNewspaperId] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItemById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetLibraryItemById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItems]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetLibraryItems] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetLibraryItemsByTitle]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetLibraryItemsByTitle] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetNewspaperById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetNewspaperById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapers]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetNewspapers] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetNewspapersById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetNewspapersById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetPatentByAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetPatentByAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetPatentById]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetPatentById] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetPatents]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetPatents] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetRoles] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetSortedLibraryItemsByYear]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetSortedLibraryItemsByYear] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetSortedLibraryItemsByYearDesc]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetSortedLibraryItemsByYearDesc] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[GetUsers] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[SoftDeleteBook]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SoftDeleteBook]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id AND Deleted = 0)
			IF(@LibraryType = 'Book')
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1)
				BEGIN
					SET @CntDeleteRow = 0 
					ROLLBACK TRANSACTION
					RETURN
				END
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'Attempt to delete an book item by external')
			END
			ELSE
			BEGIN
				SET @CntDeleteRow = 0
				ROLLBACK TRANSACTION
				RETURN
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
ALTER AUTHORIZATION ON [dbo].[SoftDeleteBook] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[SoftDeleteIssue]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SoftDeleteIssue]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	DECLARE @IdNews int
	BEGIN TRY
		BEGIN TRAN
			IF(EXISTS (SELECT ID FROM LibraryItems WHERE Id = @Id AND LibraryType = 'Issue' AND Deleted = 0))
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ('Issue', @Id,  N'Attempt to delete an issue item by external')
				IF(@CntDeleteRow <> 1)
				BEGIN
					SET @CntDeleteRow = 0
					ROLLBACK TRANSACTION
					RETURN
				END
				SELECT @IdNews = Newspaper_Id FROM Issues WHERE Id = @Id
				IF(NOT EXISTS (SELECT LibraryItems.Id FROM LibraryItems, Issues WHERE LibraryItems.Deleted = 0 AND Issues.Newspaper_Id = @IdNews AND LibraryItems.Id = Issues.Id))
				BEGIN
					UPDATE Newspapers SET Deleted = 1 WHERE Id = @IdNews
					SET @CntDeleteRow = @@ROWCOUNT
					INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ('Newspaper', @IdNews,  N'Attempt to delete an newspaper item by external')
					IF(@CntDeleteRow <> 1)
					BEGIN
						SET @CntDeleteRow = 0
						ROLLBACK TRANSACTION
						RETURN
					END
				END
			END
			ELSE
			BEGIN
				SET @CntDeleteRow = 0
				ROLLBACK TRANSACTION
				RETURN
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
ALTER AUTHORIZATION ON [dbo].[SoftDeleteIssue] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[SoftDeleteNewspaper]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SoftDeleteNewspaper]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	DECLARE @CntIssueRow int
	DECLARE @CntDeleteIssueRow int
	BEGIN TRY
		SET @CntIssueRow = 0
		SET @CntDeleteIssueRow = 0
		BEGIN TRAN
			IF(EXISTS (SELECT ID FROM Newspapers WHERE Id = @Id AND Deleted = 0))
			BEGIN
				UPDATE Newspapers SET Deleted = 1 WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1)
				BEGIN 
					SET @CntDeleteRow = 0
					ROLLBACK TRANSACTION
					RETURN
				END
				SELECT @CntIssueRow = COUNT(Id) FROM Issues WHERE Newspaper_Id = @Id
				IF(@CntIssueRow > 0)
				BEGIN
					UPDATE LibraryItems SET Deleted = 1 FROM (SELECT Id FROM Issues WHERE Newspaper_Id = @Id) AS Selected
					WHERE LibraryItems.Id = Selected.Id
					SET @CntDeleteIssueRow = @@ROWCOUNT
					IF(@CntDeleteIssueRow <> @CntIssueRow)
					BEGIN 
						SET @CntDeleteRow = 0
						ROLLBACK TRANSACTION
						RETURN
					END
				END
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES ('Newspaper', @Id,  N'Attempt to delete an newspaper item by external')
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) SELECT 'Issue', Id, N'Attempt to delete an issue item by external' FROM Issues WHERE Newspaper_Id = @Id
			END
			ELSE
			BEGIN 
				SET @CntDeleteRow = 0
				ROLLBACK TRANSACTION
				RETURN
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
ALTER AUTHORIZATION ON [dbo].[SoftDeleteNewspaper] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[SoftDeletePatent]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SoftDeletePatent]
	@Id int,
	@CntDeleteRow int output
AS
BEGIN
SET XACT_ABORT, NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
			DECLARE @LibraryType nvarchar(50) = (SELECT LibraryType FROM LibraryItems WHERE Id = @Id AND Deleted = 0)
			IF(@LibraryType = 'Patent')
			BEGIN
				UPDATE LibraryItems SET Deleted = 1 WHERE Id = @Id
				SET @CntDeleteRow = @@ROWCOUNT
				IF(@CntDeleteRow <> 1) 
				BEGIN 
					SET @CntDeleteRow = 0
					ROLLBACK TRANSACTION
					RETURN
				END
				INSERT INTO Logs(ObjectType, ObjectId, Desciption) VALUES (@LibraryType, @Id,  N'Attempt to delete an patent item by external')
			END
			ELSE
			BEGIN 
				SET @CntDeleteRow = 0
				ROLLBACK TRANSACTION
				RETURN
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
ALTER AUTHORIZATION ON [dbo].[SoftDeletePatent] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[UpdateAuthor]    Script Date: 5/9/2020 7:31:48 PM ******/
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
		IF(@CntUpdateRow <> 1) 
		BEGIN
			ROLLBACK TRANSACTION
			RETURN @CntUpdateRow
		END

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
ALTER AUTHORIZATION ON [dbo].[UpdateAuthor] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateBook]
	@Id int,
	@Title nvarchar(300),
	@PagesCount int,
	@Commentary nvarchar(2000) = NULL,
	@LibraryType nvarchar(50),

	@City nvarchar(200),
	@PublishingCompany nvarchar(200),
	@ISBN nvarchar(200) = NULL,
	@YearOfPublishing int,

	@AuthorsId nvarchar(max) = NULL,

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
ALTER AUTHORIZATION ON [dbo].[UpdateBook] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[UpdateIssue]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[UpdateIssue] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[UpdateNewspaper]    Script Date: 5/9/2020 7:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateNewspaper]
	@Id int,
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
		IF(@CntUpdateRow <> 1) 
		BEGIN
			ROLLBACK TRANSACTION
			RETURN @CntUpdateRow
		END
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
ALTER AUTHORIZATION ON [dbo].[UpdateNewspaper] TO  SCHEMA OWNER 
GO
/****** Object:  StoredProcedure [dbo].[UpdatePatent]    Script Date: 5/9/2020 7:31:48 PM ******/
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
ALTER AUTHORIZATION ON [dbo].[UpdatePatent] TO  SCHEMA OWNER 
GO
/****** Object:  Trigger [dbo].[LibraryItemAddingLogging]    Script Date: 5/9/2020 7:31:48 PM ******/
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
