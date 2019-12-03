USE [master]
GO

/****** Object:  Database [ToDoList]    Script Date: 2.12.2019 12:21:33 ******/
DROP DATABASE [ToDoList]
GO

/****** Object:  Database [ToDoList]    Script Date: 2.12.2019 12:21:33 ******/
CREATE DATABASE [ToDoList]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToDoList', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ToDoList.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToDoList_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ToDoList_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToDoList].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ToDoList] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ToDoList] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ToDoList] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ToDoList] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ToDoList] SET ARITHABORT OFF 
GO

ALTER DATABASE [ToDoList] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ToDoList] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ToDoList] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ToDoList] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ToDoList] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ToDoList] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ToDoList] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ToDoList] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ToDoList] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ToDoList] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ToDoList] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ToDoList] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ToDoList] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ToDoList] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ToDoList] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ToDoList] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ToDoList] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ToDoList] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ToDoList] SET  MULTI_USER 
GO

ALTER DATABASE [ToDoList] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ToDoList] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ToDoList] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ToDoList] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ToDoList] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ToDoList] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ToDoList] SET  READ_WRITE 
GO


----------Table Create------
USE [ToDoList]
GO

ALTER TABLE [dbo].[ToDoListItems] DROP CONSTRAINT [FK_ToDoListItems_TodoLists]
GO

/****** Object:  Table [dbo].[ToDoListItems]    Script Date: 2.12.2019 12:22:11 ******/
DROP TABLE [dbo].[ToDoListItems]
GO

/****** Object:  Table [dbo].[ToDoListItems]    Script Date: 2.12.2019 12:22:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDoListItems](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ListItemName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Deadline] [datetime] NULL,
	[Status] [char](1) NULL,
	[IsActive] [char](1) NULL,
	[ListId] [int] NULL,
 CONSTRAINT [PK_ToDoListItems] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ToDoListItems]  WITH CHECK ADD  CONSTRAINT [FK_ToDoListItems_TodoLists] FOREIGN KEY([ListId])
REFERENCES [dbo].[TodoLists] ([ListId])
GO

ALTER TABLE [dbo].[ToDoListItems] CHECK CONSTRAINT [FK_ToDoListItems_TodoLists]
GO


-----------------------------------------------------------------------------
USE [ToDoList]
GO

/****** Object:  Table [dbo].[TodoLists]    Script Date: 2.12.2019 12:22:33 ******/
DROP TABLE [dbo].[TodoLists]
GO

/****** Object:  Table [dbo].[TodoLists]    Script Date: 2.12.2019 12:22:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TodoLists](
	[ListId] [int] IDENTITY(1,1) NOT NULL,
	[ListName] [nvarchar](max) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_TodoLists] PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


------------------------------------------------------------------------------
USE [ToDoList]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2.12.2019 12:22:55 ******/
DROP TABLE [dbo].[Users]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2.12.2019 12:22:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[NameSurname] [nvarchar](150) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


------------------------------------------------------------------

USE [ToDoList]
GO

ALTER TABLE [dbo].[UsersOfToDoLists] DROP CONSTRAINT [FK_UsersOfToDoLists_Users]
GO

ALTER TABLE [dbo].[UsersOfToDoLists] DROP CONSTRAINT [FK_UsersOfToDoLists_TodoLists]
GO

/****** Object:  Table [dbo].[UsersOfToDoLists]    Script Date: 2.12.2019 12:23:10 ******/
DROP TABLE [dbo].[UsersOfToDoLists]
GO

/****** Object:  Table [dbo].[UsersOfToDoLists]    Script Date: 2.12.2019 12:23:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersOfToDoLists](
	[UserListId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ListId] [int] NULL,
 CONSTRAINT [PK_UsersOfToDoLists] PRIMARY KEY CLUSTERED 
(
	[UserListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UsersOfToDoLists]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfToDoLists_TodoLists] FOREIGN KEY([ListId])
REFERENCES [dbo].[TodoLists] ([ListId])
GO

ALTER TABLE [dbo].[UsersOfToDoLists] CHECK CONSTRAINT [FK_UsersOfToDoLists_TodoLists]
GO

ALTER TABLE [dbo].[UsersOfToDoLists]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfToDoLists_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[UsersOfToDoLists] CHECK CONSTRAINT [FK_UsersOfToDoLists_Users]
GO


--------------------------------------------------------------------------------------

-----------------------------------------

----Insert for Example

IF NOT EXISTS(select * from dbo.TodoLists where ListName='Books to read')
BEGIN
insert into dbo.TodoLists values('Books to read','1')
END
GO
IF NOT EXISTS(select * from dbo.TodoLists where ListName='Sights')
BEGIN
insert into dbo.TodoLists values('Sights','1')
END
GO
IF NOT EXISTS(select * from dbo.TodoLists where ListName='Foreign Language')
BEGIN
insert into dbo.TodoLists values('Foreign Language','1')
END
GO
----------------------------------------------------------------------
IF NOT EXISTS(select * from dbo.ToDoListItems where ListItemName='What I believe')
BEGIN
insert into dbo.ToDoListItems values('What I believe','
Philosophy Book','2019-05-12 00:00:00',1,1,1)
END
GO
IF NOT EXISTS(select * from dbo.ToDoListItems where ListItemName='Blindness')
BEGIN
insert into dbo.ToDoListItems values('Blindness','
Philosophy Book','2019-05-12 00:00:00',1,1,1)
END
GO
---------------------------------------------------------------------------
IF NOT EXISTS(select * from dbo.Users where Username='pinar')
BEGIN
insert into dbo.Users values('pinar','
Pýnar Koþan','prtXzGopiw4=','pinarkosan@yandex.com')
END
GO
---------------------------------------------------------------------------
IF NOT EXISTS(select * from dbo.UsersOfToDoLists where UserId=1)
BEGIN
insert into dbo.UsersOfToDoLists values(1,1)
END
GO



