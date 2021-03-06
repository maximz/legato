USE [master]
GO
/****** Object:  Database [legato]    Script Date: 06/24/2012 15:04:01 ******/
CREATE DATABASE [legato] ON  PRIMARY 
( NAME = N'legato', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\legato.mdf' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'legato_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\legato_log.ldf' , SIZE = 3840KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [legato] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [legato].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [legato] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [legato] SET ANSI_NULLS OFF
GO
ALTER DATABASE [legato] SET ANSI_PADDING OFF
GO
ALTER DATABASE [legato] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [legato] SET ARITHABORT OFF
GO
ALTER DATABASE [legato] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [legato] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [legato] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [legato] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [legato] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [legato] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [legato] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [legato] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [legato] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [legato] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [legato] SET  DISABLE_BROKER
GO
ALTER DATABASE [legato] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [legato] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [legato] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [legato] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [legato] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [legato] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [legato] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [legato] SET  READ_WRITE
GO
ALTER DATABASE [legato] SET RECOVERY FULL
GO
ALTER DATABASE [legato] SET  MULTI_USER
GO
ALTER DATABASE [legato] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [legato] SET DB_CHAINING OFF
GO
USE [legato]
GO
/****** Object:  User [legatouser]    Script Date: 06/24/2012 15:04:01 ******/
CREATE USER [legatouser] FOR LOGIN [legatouser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Role [aspnet_Membership_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Membership_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Membership_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Membership_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Personalization_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Personalization_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Profile_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Profile_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Profile_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Roles_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Roles_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_Roles_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_WebEvent_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [aspnet_WebEvent_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [db_executor]    Script Date: 06/24/2012 15:04:01 ******/
CREATE ROLE [db_executor] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess] AUTHORIZATION [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Roles_FullAccess] AUTHORIZATION [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess] AUTHORIZATION [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess] AUTHORIZATION [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Profile_FullAccess] AUTHORIZATION [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess] AUTHORIZATION [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess] AUTHORIZATION [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess] AUTHORIZATION [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Membership_FullAccess] AUTHORIZATION [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 06/24/2012 15:04:01 ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess] AUTHORIZATION [aspnet_Membership_BasicAccess]
GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications] 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'legato', N'legato', N'b3f3acab-babc-4630-8d11-0d4e20f754a4', NULL)
/****** Object:  Table [dbo].[InstrumentTypes]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentTypes](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[AllowedInPublic] [bit] NULL,
	[AllowedInRent] [bit] NULL,
	[AllowedInSale] [bit] NULL,
 CONSTRAINT [PK_InstrumentTypes] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentTypes] ON [dbo].[InstrumentTypes] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[InstrumentTypes] ON
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (1, N'Accordion', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (2, N'Bagpipes', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (3, N'Banjo', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (58, N'Baritone', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (59, N'Bass guitar', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (60, N'Bassoon', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (61, N'Cello', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (62, N'Clarinet', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (63, N'Double bass', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (64, N'English horn', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (65, N'Flute', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (66, N'French horn', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (67, N'Guitar', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (68, N'Harmonica', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (69, N'Harp', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (70, N'Harpsichord', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (71, N'Oboe', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (72, N'Organ', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (73, N'Percussion', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (74, N'Piano', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (75, N'Piccolo', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (76, N'Recorder', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (77, N'Saxophone', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (78, N'Sitar', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (79, N'Synth', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (80, N'Trombone', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (81, N'Trumpet', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (82, N'Tuba', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (83, N'Vibraphone', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (84, N'Viola', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (85, N'Violin', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (86, N'Xylophone', 1, 1, 1)
INSERT [dbo].[InstrumentTypes] ([TypeID], [Name], [AllowedInPublic], [AllowedInRent], [AllowedInSale]) VALUES (87, N'Other', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[InstrumentTypes] OFF
/****** Object:  Table [dbo].[OpenIDWhiteList]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpenIDWhiteList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OpenID] [nvarchar](450) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_OpenIDWhiteList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OpenIDWhiteList] ON [dbo].[OpenIDWhiteList] 
(
	[OpenID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OneTimeRegistrationCodes]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OneTimeRegistrationCodes](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomWelcomeName] [nvarchar](400) NULL,
	[UsesRemaining] [int] NOT NULL,
 CONSTRAINT [PK_OneTimeRegistrationCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MiniProfilerResults]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MiniProfilerResults](
	[Id] [uniqueidentifier] NOT NULL,
	[Results] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_MiniProfilerResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[MiniProfilerResults] ([Id], [Results]) VALUES (N'ff0872fa-12b6-474e-8bfa-09fc3731b042', 0x0A1209FA7208FFB6124E47118BFA09FC3731B042120D2F64656661756C742E617370781A0B08B2C9CDA8E6D3BA2E1005220C4D4158494D2D4C4150544F50328D010A1209B4080D812619214511A5B29C3847E20FFB1223687474703A2F2F6C6F63616C686F73743A31373739392F64656661756C742E61737078199A9999995945D440219A9999999999F13F2A400A1209D0BE5D18475F264711A0DB3B96C7B7EE4612184170706C69636174696F6E5F426567696E52657175657374199A9999999999E93F2100000000008049400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[MiniProfilerResults] ([Id], [Results]) VALUES (N'4026be64-e200-4e94-ad4b-889f1bff1223', 0x0A120964BE264000E2944E11AD4B889F1BFF12231212496E737472756D656E74732F5375626D69741A0B08CC83FEBFDCD1892F100522094C494E45425245414B32A4020A12093A348F646B0BFB49119D5BB70344E1F7671232687474703A2F2F6465762E6C656761746F6E6574776F726B2E636F6D3A38302F696E737472756D656E74732F7375626D69741966666666660EA2402A400A1209CB577DEF7F659C40119B2569B608D48D0012184170706C69636174696F6E5F426567696E52657175657374190000000000000000219A9999999999B93F3A8E010803126753454C454354205B74305D2E5B5479706549445D204153205B49645D2C205B74305D2E5B4E616D655D0D0A46524F4D205B64626F5D2E5B496E737472756D656E7454797065735D204153205B74305D0D0A4F52444552204259205B74305D2E5B5479706549445D1A065375626D6974216666666666662140299A9999999999E93F31000000000000E03F000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[MiniProfilerResults] ([Id], [Results]) VALUES (N'278d6949-8b9e-497f-a2a5-f7c263f49470', 0x0A120949698D279E8B7F4911A2A5F7C263F49470120D2F64656661756C742E617370781A0B08D8C8BEBA8BA7BA2E1005220C4D4158494D2D4C4150544F50328D010A1209579DDA79AA79824611B3A9D6AD5CC372A61223687474703A2F2F6C6F63616C686F73743A31373739392F64656661756C742E61737078199A999999195BB54021333333333333E33F2A400A1209600F8630F509FC4B1196C2C65EF19C3FE012184170706C69636174696F6E5F426567696E52657175657374199A9999999999D93F2100000000000010400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
/****** Object:  Table [dbo].[VoteTypes]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VoteTypes](
	[VoteTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[RepImpact] [int] NOT NULL,
 CONSTRAINT [PK_VoteTypes] PRIMARY KEY CLUSTERED 
(
	[VoteTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 06/24/2012 15:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'1231fb58-ff8c-4b8a-b447-cf631cca0a5c', N'eyulaeva', N'eyulaeva', NULL, 0, CAST(0x00009F230027D26F AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'maximz', N'maximz', NULL, 0, CAST(0x0000A079011D1595 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'9a70a1ea-d87e-4a24-9a4b-cebe1ab9f7dd', N'mootesterz', N'mootesterz', NULL, 0, CAST(0x00009F6C00067D9D AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'e61b4705-d0ce-43bc-b0de-122b297cdbfb', N'mootesterz1', N'mootesterz1', NULL, 0, CAST(0x00009F6C00077EB8 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', N'mzprofile', N'mzprofile', NULL, 0, CAST(0x00009FAD0014C3F8 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'fc376fa1-7667-4f24-848b-18e7f084805e', N'testerz1', N'testerz1', NULL, 0, CAST(0x00009FC201428B16 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'b1e94788-876a-4b8f-9860-0534fc521d7b', N'vshevche', N'vshevche', NULL, 0, CAST(0x00009FC800A36665 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'a3529a95-7f3d-4198-8dda-fb0368689937', N'zaatkin', N'zaatkin', NULL, 0, CAST(0x00009F6C000A22C4 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'8936047e-cd70-4ef6-a14b-31cea4d2fd2d', N'zaatkinz', N'zaatkinz', NULL, 0, CAST(0x00009F6C000B80FE AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'020f0ae8-607d-4951-8265-6c5a390e9bc4', N'zazius1', N'zazius1', NULL, 0, CAST(0x00009F6C0008D35F AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'4c55c04f-6fe7-442e-84de-e4c43803a666', N'zinax2', N'zinax2', NULL, 0, CAST(0x00009FC801313B4A AS DateTime))
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 06/24/2012 15:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END
GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END
GO
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Paths_index] ON [dbo].[aspnet_Paths] 
(
	[ApplicationId] ASC,
	[LoweredPath] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END
GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Roles_index1] ON [dbo].[aspnet_Roles] 
(
	[ApplicationId] ASC,
	[LoweredRoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'067142f0-2478-4143-bf8e-b453b9a185be', N'ActiveUser', N'activeuser', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'296bf6b9-a965-477b-8ce7-dbdc2265f7f1', N'Administrator', N'administrator', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'cc38649d-b3f6-4838-81c8-436450221000', N'EmailNotConfirmed', N'emailnotconfirmed', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'0f84c5b8-5975-4652-886d-c9f50495a639', N'Moderator', N'moderator', NULL)
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_PersonalizationPerUser_index1] ON [dbo].[aspnet_PersonalizationPerUser] 
(
	[PathId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [aspnet_PersonalizationPerUser_ncindex2] ON [dbo].[aspnet_PersonalizationPerUser] 
(
	[UserId] ASC,
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'b1e94788-876a-4b8f-9860-0534fc521d7b', N'AboutMe:B:0:-1:FullName:S:0:8:ReinstateDate:S:8:81:', N'Valentin<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009FA6004E36FB AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'e61b4705-d0ce-43bc-b0de-122b297cdbfb', N'FullName:S:0:11:ReinstateDate:S:11:81:', N'Tester MooZ<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F6C00077EB8 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'fc376fa1-7667-4f24-848b-18e7f084805e', N'AboutMe:S:0:10:FullName:S:10:9:ReinstateDate:S:19:81:', N'Tester Z 1Tester Z1<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009FC20141FB48 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', N'AboutMe:S:0:13:FullName:S:13:2:ReinstateDate:S:15:81:', N'I''m a tester.MZ<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009FAC01782449 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'8936047e-cd70-4ef6-a14b-31cea4d2fd2d', N'AboutMe:B:0:-1:FullName:S:0:9:ReinstateDate:S:9:81:', N'Zaatkin Z<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F6C000B80FE AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'020f0ae8-607d-4951-8265-6c5a390e9bc4', N'AboutMe:S:0:3:FullName:S:3:10:ReinstateDate:S:13:81:', N'Z1!Zazius One<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F6C00084D29 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'9a70a1ea-d87e-4a24-9a4b-cebe1ab9f7dd', N'FullName:S:0:11:ReinstateDate:S:11:81:', N'Tester MooZ<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F6C00067D9D AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'1231fb58-ff8c-4b8a-b447-cf631cca0a5c', N'FullName:S:0:13:ReinstateDate:S:13:81:', N'Elena Yulaeva<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F230027D26F AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'4c55c04f-6fe7-442e-84de-e4c43803a666', N'AboutMe:B:0:-1:FullName:S:0:14:ReinstateDate:S:14:81:', N'Ilya Zaslavsky<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009FA900EC5EE5 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'a3529a95-7f3d-4198-8dda-fb0368689937', N'FullName:S:0:9:ReinstateDate:S:9:81:', N'Zaatkin Z<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009F6C000A22C4 AS DateTime))
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'FullName:S:0:15:ReinstateDate:S:15:81:', N'Maxim Zaslavsky<?xml version="1.0" encoding="utf-16"?>
<dateTime>0001-01-01T00:00:00</dateTime>', 0x, CAST(0x00009EF7001406DD AS DateTime))
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership] 
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'1231fb58-ff8c-4b8a-b447-cf631cca0a5c', N'5lTHskQ52xMNib9JihV3Lo55A90=', 1, N'kaHkSeENRCYSf0CZHf2KLQ==', NULL, N'eyulaeva@gmail.com', N'eyulaeva@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F230027C66C AS DateTime), CAST(0x00009F230027C66C AS DateTime), CAST(0x00009F230027C66C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'9a70a1ea-d87e-4a24-9a4b-cebe1ab9f7dd', N'nWWh08iYgB3uksLm/UZ27OWEH9s=', 1, N'E0U9JMFmt4vnE96hJzY83Q==', NULL, N'maximz.fw+testerz@gmail.com', N'maximz.fw+testerz@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F6C00067908 AS DateTime), CAST(0x00009F6C00067908 AS DateTime), CAST(0x00009F6C00067908 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'e61b4705-d0ce-43bc-b0de-122b297cdbfb', N'KH5qGtsB+Md5EkeSsBAWzpEP4CE=', 1, N'1FHnC3MY/uIJNMZWLqXGAQ==', NULL, N'maximz.fw+testerz@gmail.com', N'maximz.fw+testerz@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F6C00076944 AS DateTime), CAST(0x00009F6C00076944 AS DateTime), CAST(0x00009F6C00076944 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'fc376fa1-7667-4f24-848b-18e7f084805e', N'Mj35n5wSx/tlSbSNrCYGuRDAFRQ=', 1, N'Z2oCj0v/rlpAmGq7mhYT1w==', NULL, N'maximz.fw+testerz1@gmail.com', N'maximz.fw+testerz1@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FC20141FA2C AS DateTime), CAST(0x00009FC20141FA2C AS DateTime), CAST(0x00009FC20141FA2C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'020f0ae8-607d-4951-8265-6c5a390e9bc4', N'IJSXHfS3kX+vCtL5l4EsR5EBH2c=', 1, N'sa+dbCXNSWQwlBZ29tdX1Q==', NULL, N'maximz.fw+z1@gmail.com', N'maximz.fw+z1@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F6C000846C0 AS DateTime), CAST(0x00009F6C000846C0 AS DateTime), CAST(0x00009F6C000846C0 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', N'GZPgyWRt6W5cO9eGuGV5YIIA8qw=', 1, N'PrRbO+0r+UR9i8AjAQlBUQ==', NULL, N'maximz.profile@gmail.com', N'maximz.profile@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FAC017823CC AS DateTime), CAST(0x00009FAC017823CC AS DateTime), CAST(0x00009FAC017823CC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'o+8HvEhGIhm6d0Udv8GTINqWZTA=', 1, N'6rcZ+FM0LY+S90ppW+J/rg==', NULL, N'maximz2005+maximzopenid@gmail.com', N'maximz2005+maximzopenid@gmail.com', NULL, NULL, 1, 0, CAST(0x00009EF7001405C8 AS DateTime), CAST(0x00009EF7001405C8 AS DateTime), CAST(0x00009EF7001405C8 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'b1e94788-876a-4b8f-9860-0534fc521d7b', N'lZAyXveb8DhraKmqD4RJo5NAuKE=', 1, N'pKUYTrgjKsnVb+EO/Iswkg==', NULL, N'vshevche@gmail.com', N'vshevche@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FA6004E36A8 AS DateTime), CAST(0x00009FA6004E36A8 AS DateTime), CAST(0x00009FA6004E36A8 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'a3529a95-7f3d-4198-8dda-fb0368689937', N'gg7Ug6ODcHXeVzXjzWG6jku96ro=', 1, N'DGN4DF/ydLTkD0Ds+z225g==', NULL, N'zaatkin@gmail.com', N'zaatkin@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F6C000A215C AS DateTime), CAST(0x00009F6C000A215C AS DateTime), CAST(0x00009F6C000A215C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'8936047e-cd70-4ef6-a14b-31cea4d2fd2d', N'19bSEZ0aTct2K5+uGQq+ItzBqMk=', 1, N'TzawBUKqQnv8JonWYeb1Xw==', NULL, N'zaatkin+z@gmail.com', N'zaatkin+z@gmail.com', NULL, NULL, 1, 0, CAST(0x00009F6C000B63A0 AS DateTime), CAST(0x00009F6C000B63A0 AS DateTime), CAST(0x00009F6C000B63A0 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'b3f3acab-babc-4630-8d11-0d4e20f754a4', N'4c55c04f-6fe7-442e-84de-e4c43803a666', N'0jJ5js34u5Uunn7wXHQFpNZC0H0=', 1, N'jEWrAWL4j1dDWTA6qHh5Jg==', NULL, N'zinax2@yahoo.com', N'zinax2@yahoo.com', NULL, NULL, 1, 0, CAST(0x00009FA900EC5DC4 AS DateTime), CAST(0x00009FA900EC5DC4 AS DateTime), CAST(0x00009FA900EC5DC4 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instruments]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instruments](
	[InstrumentID] [int] IDENTITY(1,1) NOT NULL,
	[ListingClass] [varchar](10) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Lat] [float] NOT NULL,
	[Long] [float] NOT NULL,
	[StreetAddress] [nvarchar](max) NOT NULL,
	[VenueName] [nvarchar](200) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[SubmissionDate] [datetime] NOT NULL,
	[Brand] [nvarchar](100) NULL,
	[Model] [nvarchar](100) NULL,
	[ListingViews] [int] NOT NULL,
	[Price] [money] NULL,
	[TimeSpanOfPrice] [varchar](15) NULL,
	[GlobalPostID] [int] NULL,
	[Markdown] [nvarchar](max) NULL,
	[HTML] [nvarchar](max) NULL,
	[DisplayedStreetAddress] [nvarchar](max) NULL,
	[AddressPrivacy] [int] NOT NULL,
	[DisplayedLat] [float] NULL,
	[DisplayedLong] [float] NULL,
 CONSTRAINT [PK_Instruments] PRIMARY KEY CLUSTERED 
(
	[InstrumentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_Instruments] ON [dbo].[Instruments] 
(
	[ListingClass] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Instruments_1] ON [dbo].[Instruments] 
(
	[TypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Instruments_2] ON [dbo].[Instruments] 
(
	[ListingClass] ASC,
	[TypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Instruments_3] ON [dbo].[Instruments] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Instruments_4] ON [dbo].[Instruments] 
(
	[SubmissionDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Instruments] ON
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (1, N'1', 1, 33, -117, N'3005 Governor Drive, 92122', N'My house', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F1C001871D0 AS DateTime), N'Steinway', N'Upright', 23, 5.0000, N'month', 1, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (8, N'2', 2, 32.850533, -117.220513, N'3005 Governor Drive', N'Zaslavsky', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F2400B0BD73 AS DateTime), N'yamaha', N'Alto', 7, 3.0000, N'hour', 7, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (9, N'2', 1, 32.850533, -117.220513, N'3005 Governor Drive, 92122', N'NYC', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5010683B1 AS DateTime), NULL, NULL, 2, 5.0000, N'day', 10, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (10, N'1', 2, 32.850533, -117.220513, N'3005 Governor Drive, 92122', N'NYC', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA50107D6C6 AS DateTime), NULL, NULL, 13, 5.0000, N'day', 13, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (11, N'2', 1, 48.856614, 2.3522219, N'Paris, France', N'Le Bistro', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5011A8359 AS DateTime), NULL, NULL, 18, 5.0000, N'month', 16, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (12, N'1', 2, 40.7528622, -73.9573661, N'1 Main Street, New York, 10044', N'Empire State Building', N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FA50156B186 AS DateTime), NULL, NULL, 15, NULL, NULL, 19, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (13, N'1', 1, 21.3069444, -157.8583333, N'Marriott, Honolulu', N'Honolulu, HI', N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FB300569748 AS DateTime), NULL, NULL, 6, NULL, NULL, 24, N'Test.', N'<p>Test.</p>', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (14, N'2', 74, 32.869923, -117.238841, N'8943 Caminito Fresco, 92037', N'A house', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FC7011C3F5F AS DateTime), NULL, NULL, 2, 15.0000, N'hour', 36, N'This instrument is old but nice.
![enter image description here][1]


  [1]: http://i.imgur.com/eqbVo.png', N'<p>This instrument is old but nice.
<img src="http://i.imgur.com/eqbVo.png" alt="enter image description here" /></p>
', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (15, N'2', 62, 40.7528622, -73.9573661, N'1 Main Street, 10044', N'Main Street!', N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC7011CCEED AS DateTime), NULL, NULL, 13, 10.0000, N'hour', 37, N'It rocks.', N'<p>It rocks.</p>
', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (16, N'1', 74, 34.0682823, -118.3985783, N'225 N Canon Dr, Beverly Hills, CA 90210 ', N'Montage bar', N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800289186 AS DateTime), NULL, NULL, 2, NULL, NULL, 41, N'A nice piano in a great bar. Probably not the right place to practice, but if you play well (especially Nordstrom-style music), and the regular pianist is not there, the staff would welcome you playing', N'<p>A nice piano in a great bar. Probably not the right place to practice, but if you play well (especially Nordstrom-style music), and the regular pianist is not there, the staff would welcome you playing</p>
', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (17, N'1', 74, 36.7407212, -119.0564661, N'44138 East Kings Canyon Road, Dunlap, CA ', N'Snowline Lodge', N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800AD62EF AS DateTime), NULL, NULL, 3, NULL, NULL, 44, N'I encountered this instrument when descending from the Sequoia Park along the Kings Canyon Road. It is on terrace of the Lodge, and appears a pretty beaten up but playable piano (if you are content on playing outside, and someone would hold the lid for you). Quite out of tune, and sounds a bit like clavesine, but if you are desperate to play after several days in the mountains -this is an option. No people where in sight, so nobody to ask if it was Ok to play.', N'<p>I encountered this instrument when descending from the Sequoia Park along the Kings Canyon Road. It is on terrace of the Lodge, and appears a pretty beaten up but playable piano (if you are content on playing outside, and someone would hold the lid for you). Quite out of tune, and sounds a bit like clavesine, but if you are desperate to play after several days in the mountains -this is an option. No people where in sight, so nobody to ask if it was Ok to play.</p>
', NULL, 0, NULL, NULL)
INSERT [dbo].[Instruments] ([InstrumentID], [ListingClass], [TypeID], [Lat], [Long], [StreetAddress], [VenueName], [UserID], [SubmissionDate], [Brand], [Model], [ListingViews], [Price], [TimeSpanOfPrice], [GlobalPostID], [Markdown], [HTML], [DisplayedStreetAddress], [AddressPrivacy], [DisplayedLat], [DisplayedLong]) VALUES (18, N'1', 74, 40.735657, -74.1723667, N'Newark, U.S.', N'Test Cello', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FF20114EA25 AS DateTime), NULL, NULL, 1, 5.0000, N'minute', 47, N'Let''s see if this works!', N'<p>Let''s see if this works!</p>
', NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Instruments] OFF
/****** Object:  Table [dbo].[GlobalPostIDs]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalPostIDs](
	[GlobalPostID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[SpecificPostID] [int] NOT NULL,
	[PostCategory] [nvarchar](50) NOT NULL,
	[SubmissionDate] [datetime] NULL,
 CONSTRAINT [PK_GlobalPostIDs] PRIMARY KEY CLUSTERED 
(
	[GlobalPostID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_GlobalPostIDs] ON [dbo].[GlobalPostIDs] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalPostIDs] ON
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 1, N'Instrument', CAST(0x00009F1C001871D0 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (2, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 3, N'Instrument', CAST(0x00009F220118C2F0 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (3, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 4, N'Instrument', CAST(0x00009F2201191467 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 5, N'Instrument', CAST(0x00009F22011A5A92 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (5, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 6, N'Instrument', CAST(0x00009F220120433F AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (6, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 7, N'Instrument', CAST(0x00009F22018B4C92 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (7, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 8, N'Instrument', CAST(0x00009F2400B0BD73 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (8, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 2, N'InstrumentReview', CAST(0x00009F2400B0BD73 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (9, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 2, N'InstrumentReviewRevision', CAST(0x00009F2400B0BD73 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (10, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 9, N'Instrument', CAST(0x00009FA5010683B1 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (11, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 3, N'InstrumentReview', CAST(0x00009FA5010683B1 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (12, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 3, N'InstrumentReviewRevision', CAST(0x00009FA5010683B1 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (13, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 10, N'Instrument', CAST(0x00009FA50107D6C6 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (14, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 4, N'InstrumentReview', CAST(0x00009FA50107D6C6 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (15, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 4, N'InstrumentReviewRevision', CAST(0x00009FA50107D6C6 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (16, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 11, N'Instrument', CAST(0x00009FA5011A8359 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (17, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 5, N'InstrumentReview', CAST(0x00009FA5011A8359 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (18, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 5, N'InstrumentReviewRevision', CAST(0x00009FA5011A8359 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (19, N'b1e94788-876a-4b8f-9860-0534fc521d7b', 12, N'Instrument', CAST(0x00009FA50156B186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (20, N'b1e94788-876a-4b8f-9860-0534fc521d7b', 6, N'InstrumentReview', CAST(0x00009FA50156B186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (21, N'b1e94788-876a-4b8f-9860-0534fc521d7b', 6, N'InstrumentReviewRevision', CAST(0x00009FA50156B186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (22, N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', 7, N'InstrumentReview', CAST(0x00009FAC011B7C77 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (23, N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', 8, N'InstrumentReview', CAST(0x00009FAC011C6FF8 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (24, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 13, N'Instrument', CAST(0x00009FB300569748 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (25, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 9, N'InstrumentReview', CAST(0x00009FB300569748 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (26, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 7, N'InstrumentReviewRevision', CAST(0x00009FB300569748 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (27, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 10, N'InstrumentReview', CAST(0x00009FBE0020A2F6 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (28, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 8, N'InstrumentReviewRevision', CAST(0x00009FBE0020A2F6 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (29, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 9, N'InstrumentReviewRevision', CAST(0x00009FBE002C489B AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (30, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 10, N'InstrumentReviewRevision', CAST(0x00009FBE002D7988 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (31, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 11, N'InstrumentReviewRevision', CAST(0x00009FBE002DFC53 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (32, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 12, N'InstrumentReviewRevision', CAST(0x00009FBE0034A8AA AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (33, N'fc376fa1-7667-4f24-848b-18e7f084805e', 1, N'Conversation', CAST(0x00009FC200BE84D7 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (34, N'fc376fa1-7667-4f24-848b-18e7f084805e', 1, N'Message', CAST(0x00009FC200BE84D7 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (35, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 2, N'Message', CAST(0x00009FC200BEA9B4 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (36, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 14, N'Instrument', CAST(0x00009FC7011C3F5F AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (37, N'b1e94788-876a-4b8f-9860-0534fc521d7b', 15, N'Instrument', CAST(0x00009FC7011CCEED AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (38, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 2, N'Conversation', CAST(0x00009FC7014FF3AB AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (39, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 3, N'Message', CAST(0x00009FC7014FF3AB AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (40, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 4, N'Message', CAST(0x00009FC701503EB2 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (41, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 16, N'Instrument', CAST(0x00009FC800289186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (42, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 11, N'InstrumentReview', CAST(0x00009FC800289186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (43, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 13, N'InstrumentReviewRevision', CAST(0x00009FC800289186 AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (44, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 17, N'Instrument', CAST(0x00009FC800AD62EF AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (45, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 12, N'InstrumentReview', CAST(0x00009FC800AD62EF AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (46, N'4c55c04f-6fe7-442e-84de-e4c43803a666', 14, N'InstrumentReviewRevision', CAST(0x00009FC800AD62EF AS DateTime))
INSERT [dbo].[GlobalPostIDs] ([GlobalPostID], [UserID], [SpecificPostID], [PostCategory], [SubmissionDate]) VALUES (47, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', 18, N'Instrument', CAST(0x00009FF20114EA25 AS DateTime))
SET IDENTITY_INSERT [dbo].[GlobalPostIDs] OFF
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  Table [dbo].[UserSuspensions]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSuspensions](
	[SuspensionID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[SuspensionDate] [datetime] NOT NULL,
	[ReinstateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PianoUserSuspensions] PRIMARY KEY CLUSTERED 
(
	[SuspensionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOpenIds]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOpenIds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OpenIdClaim] [nvarchar](450) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__UserOpen__3214EC072057CCD0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserOpenIds] ON [dbo].[UserOpenIds] 
(
	[OpenIdClaim] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserOpenIds_1] ON [dbo].[UserOpenIds] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserOpenIds] ON
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (1, N'http://maximzaslavsky.com/', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (2, N'https://www.google.com/accounts/o8/id?id=AItOawkGZ17n4_gxnjz8ORdVAuYj-HCM5RTnhRE', N'020f0ae8-607d-4951-8265-6c5a390e9bc4')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (3, N'https://www.google.com/accounts/o8/id?id=AItOawkaVagnagqFS5R7jDVfjmsufOLkESwQJm0', N'8936047e-cd70-4ef6-a14b-31cea4d2fd2d')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (4, N'https://www.google.com/accounts/o8/id?id=AItOawm4f9cBR7Xjed4SHr2ngHsYVnn51sDynmI', N'b1e94788-876a-4b8f-9860-0534fc521d7b')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (5, N'https://me.yahoo.com/a/O6vhm0J2r5hiv0FN7u1LZpDh5g--#350c3', N'4c55c04f-6fe7-442e-84de-e4c43803a666')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (6, N'https://www.google.com/accounts/o8/id?id=AItOawn3E4EZb3BCYLTQ-x1DTBPzhOFRmkO8YRY', N'67ec7bce-e8f2-4f1b-9735-2843f593c27b')
INSERT [dbo].[UserOpenIds] ([Id], [OpenIdClaim], [UserId]) VALUES (7, N'https://www.google.com/accounts/o8/id?id=AItOawlJv5YEoMKG-gGFTqEAr0oHm02WrMeYwMk', N'fc376fa1-7667-4f24-848b-18e7f084805e')
SET IDENTITY_INSERT [dbo].[UserOpenIds] OFF
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
GO
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Paths]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'e61b4705-d0ce-43bc-b0de-122b297cdbfb', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'fc376fa1-7667-4f24-848b-18e7f084805e', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'9a70a1ea-d87e-4a24-9a4b-cebe1ab9f7dd', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'1231fb58-ff8c-4b8a-b447-cf631cca0a5c', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'4c55c04f-6fe7-442e-84de-e4c43803a666', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'a3529a95-7f3d-4198-8dda-fb0368689937', N'cc38649d-b3f6-4838-81c8-436450221000')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'b1e94788-876a-4b8f-9860-0534fc521d7b', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'e61b4705-d0ce-43bc-b0de-122b297cdbfb', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'fc376fa1-7667-4f24-848b-18e7f084805e', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'67ec7bce-e8f2-4f1b-9735-2843f593c27b', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'8936047e-cd70-4ef6-a14b-31cea4d2fd2d', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'020f0ae8-607d-4951-8265-6c5a390e9bc4', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'9a70a1ea-d87e-4a24-9a4b-cebe1ab9f7dd', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'1231fb58-ff8c-4b8a-b447-cf631cca0a5c', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'4c55c04f-6fe7-442e-84de-e4c43803a666', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'a3529a95-7f3d-4198-8dda-fb0368689937', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'067142f0-2478-4143-bf8e-b453b9a185be')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'0f84c5b8-5975-4652-886d-c9f50495a639')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'296bf6b9-a965-477b-8ce7-dbdc2265f7f1')
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_User]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Shared]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
GO
/****** Object:  Table [dbo].[Votes]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votes](
	[VoteID] [int] IDENTITY(1,1) NOT NULL,
	[VoterID] [uniqueidentifier] NOT NULL,
	[UserAffectedByVoteID] [uniqueidentifier] NULL,
	[PostID] [int] NOT NULL,
	[VoteType] [int] NOT NULL,
	[VoteDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED 
(
	[VoteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Votes] ON [dbo].[Votes] 
(
	[UserAffectedByVoteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Votes_1] ON [dbo].[Votes] 
(
	[PostID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Votes_2] ON [dbo].[Votes] 
(
	[PostID] ASC,
	[VoteType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
GO
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
GO
/****** Object:  Table [dbo].[ResetPasswordRecords]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResetPasswordRecords](
	[ResetID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ResetPasswordRecords] PRIMARY KEY CLUSTERED 
(
	[ResetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[GlobalPostID] [int] NOT NULL,
	[IsUnread] [bit] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Notifications] ON [dbo].[Notifications] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Notifications_1] ON [dbo].[Notifications] 
(
	[UserID] ASC,
	[Date] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Date], [GlobalPostID], [IsUnread]) VALUES (1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FC200BE84D7 AS DateTime), 33, 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Date], [GlobalPostID], [IsUnread]) VALUES (2, N'fc376fa1-7667-4f24-848b-18e7f084805e', CAST(0x00009FC200BEA9B4 AS DateTime), 35, 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Date], [GlobalPostID], [IsUnread]) VALUES (3, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC7014FF3AB AS DateTime), 38, 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Date], [GlobalPostID], [IsUnread]) VALUES (4, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC701503EB2 AS DateTime), 40, 0)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
/****** Object:  Table [dbo].[InstrumentHours]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentHours](
	[HoursID] [int] IDENTITY(1,1) NOT NULL,
	[InstrumentID] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[OpenTime] [time](7) NULL,
	[CloseTime] [time](7) NULL,
 CONSTRAINT [PK_InstrumentHours] PRIMARY KEY CLUSTERED 
(
	[HoursID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentHours] ON [dbo].[InstrumentHours] 
(
	[InstrumentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[InstrumentHours] ON
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (1, 1, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (2, 1, 1, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (3, 1, 2, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (4, 1, 3, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (5, 1, 4, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (6, 1, 5, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (7, 1, 6, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (8, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (9, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (10, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (11, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (12, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (13, 8, 0, NULL, NULL)
INSERT [dbo].[InstrumentHours] ([HoursID], [InstrumentID], [Day], [OpenTime], [CloseTime]) VALUES (14, 8, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InstrumentHours] OFF
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Conversations](
	[ConversationID] [int] IDENTITY(1,1) NOT NULL,
	[User1] [uniqueidentifier] NOT NULL,
	[User2] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[LastMessageDate] [datetime] NULL,
	[Subject] [varchar](100) NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_Conversations] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_Conversations] ON [dbo].[Conversations] 
(
	[User1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Conversations_1] ON [dbo].[Conversations] 
(
	[User2] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Conversations] ON
INSERT [dbo].[Conversations] ([ConversationID], [User1], [User2], [StartDate], [LastMessageDate], [Subject], [GlobalPostID]) VALUES (1, N'fc376fa1-7667-4f24-848b-18e7f084805e', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FC200BE84D7 AS DateTime), CAST(0x00009FC200BEA9B4 AS DateTime), N'Site feedback', 33)
INSERT [dbo].[Conversations] ([ConversationID], [User1], [User2], [StartDate], [LastMessageDate], [Subject], [GlobalPostID]) VALUES (2, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC7014FF3AB AS DateTime), CAST(0x00009FC701503EB2 AS DateTime), N'Re: Clarinet: 1 Main Street, 10044', 38)
SET IDENTITY_INSERT [dbo].[Conversations] OFF
/****** Object:  Table [dbo].[ConfirmEmailAddresses]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfirmEmailAddresses](
	[ConfirmID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConfirmEmailAddresses] PRIMARY KEY CLUSTERED 
(
	[ConfirmID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ConfirmEmailAddresses] ([ConfirmID], [UserID]) VALUES (N'90f3f54a-0391-4420-8db9-233e83ae07b9', N'67ec7bce-e8f2-4f1b-9735-2843f593c27b')
INSERT [dbo].[ConfirmEmailAddresses] ([ConfirmID], [UserID]) VALUES (N'9ea30bd1-0dba-4ee6-aa0d-ab5980e26360', N'4c55c04f-6fe7-442e-84de-e4c43803a666')
INSERT [dbo].[ConfirmEmailAddresses] ([ConfirmID], [UserID]) VALUES (N'c318b0b9-e394-4ac3-b73e-e5f206420971', N'fc376fa1-7667-4f24-848b-18e7f084805e')
/****** Object:  Table [dbo].[InstrumentReviews]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentReviews](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[InstrumentID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[SubmissionDate] [datetime] NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_InstrumentReviews] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviews] ON [dbo].[InstrumentReviews] 
(
	[InstrumentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviews_1] ON [dbo].[InstrumentReviews] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviews_2] ON [dbo].[InstrumentReviews] 
(
	[SubmissionDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[InstrumentReviews] ON
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (1, 1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F1C001871D0 AS DateTime), 1)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (2, 8, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F2400B0BD73 AS DateTime), 8)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (3, 9, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5010683B1 AS DateTime), 11)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (4, 10, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA50107D6C6 AS DateTime), 14)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (5, 11, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5011A8359 AS DateTime), 17)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (6, 12, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FA50156B186 AS DateTime), 20)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (9, 13, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FB300569748 AS DateTime), 25)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (10, 13, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE0020A2F6 AS DateTime), 27)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (11, 16, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800289186 AS DateTime), 42)
INSERT [dbo].[InstrumentReviews] ([ReviewID], [InstrumentID], [UserID], [SubmissionDate], [GlobalPostID]) VALUES (12, 17, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800AD62EF AS DateTime), 45)
SET IDENTITY_INSERT [dbo].[InstrumentReviews] OFF
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END
GO
/****** Object:  Table [dbo].[InstrumentReviewRevisions]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstrumentReviewRevisions](
	[RevisionID] [int] IDENTITY(1,1) NOT NULL,
	[ReviewID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[RevisionDate] [datetime] NOT NULL,
	[RatingGeneral] [int] NOT NULL,
	[LastUseDate] [datetime] NULL,
	[MessageMarkdown] [nvarchar](max) NOT NULL,
	[MessageHTML] [nvarchar](max) NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_InstrumentReviewRevisions] PRIMARY KEY CLUSTERED 
(
	[RevisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviewRevisions] ON [dbo].[InstrumentReviewRevisions] 
(
	[ReviewID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviewRevisions_1] ON [dbo].[InstrumentReviewRevisions] 
(
	[ReviewID] ASC,
	[RevisionDate] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_InstrumentReviewRevisions_2] ON [dbo].[InstrumentReviewRevisions] 
(
	[LastUseDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[InstrumentReviewRevisions] ON
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (1, 1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F1C001871D0 AS DateTime), 5, CAST(0x00009F1C001871D0 AS DateTime), N'Blah blah', N'<p>Blah blah</p>', 1)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (2, 2, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009F2400B0BD73 AS DateTime), 5, CAST(0x00009F1600000000 AS DateTime), N'\u003cp\u003eBlah!\u003c/p\u003e', N'<p>Blah!</p>
', 9)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (3, 3, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5010683B1 AS DateTime), 3, NULL, N'\u003cp\u003eMoo.\u003c/p\u003e', N'<p>Moo.</p>
', 12)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (4, 4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA50107D6C6 AS DateTime), 3, NULL, N'\u003cp\u003eMoo.\u003c/p\u003e', N'<p>Moo.</p>
', 15)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (5, 5, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FA5011A8359 AS DateTime), 5, NULL, N'\u003cp\u003eLovely!\u003c/p\u003e', N'<p>Lovely!</p>
', 18)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (6, 6, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FA50156B186 AS DateTime), 1, NULL, N'\u003cp\u003eThis sax sucks! \u003c/p\u003e\r\n\r\n\u003ch2\u003e\u003cstrong\u003eDON\u0027T USE IT\u003c/strong\u003e\u003c/h2\u003e', N'<p>This sax sucks! </p>

<h2><strong>DON''T USE IT</strong></h2>
', 21)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (7, 9, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FB300569748 AS DateTime), 5, NULL, N'\u003cp\u003eWhite piano\u003c/p\u003e', N'<p>White piano</p>
', 26)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (8, 10, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE0020A2F6 AS DateTime), 5, NULL, N'\u003cp\u003eNice instrument!\u003c/p\u003e', N'<p>Nice instrument!</p>
', 28)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (9, 4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE002C489B AS DateTime), 3, NULL, N'\u003cp\u003e\\u003cp\\u003eMoo.\\u003c/p\\u003e\r\nTest!\u003c/p\u003e', N'<p>\u003cp\u003eMoo.\u003c/p\u003e
Test!</p>
', 29)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (10, 4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE002D7988 AS DateTime), 5, NULL, N'\u003cp\u003emoo.\u003c/p\u003e\r\n\r\n\u003cp\u003eTest.\u003c/p\u003e', N'<p>moo.</p>

<p>Test.</p>
', 30)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (11, 4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE002DFC53 AS DateTime), 5, NULL, N'<p>test!
<em>moo</em></p>', N'<p>test!
<em>moo</em></p>
', 31)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (12, 4, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FBE0034A8AA AS DateTime), 5, NULL, N'test!
**moo**

why not?
http://google.com

 - gah!', N'<p>test!
<strong>moo</strong></p>

<p>why not?
http://google.com</p>

<ul>
<li>gah!</li>
</ul>
', 32)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (13, 11, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800289186 AS DateTime), 4, NULL, N'A nice piano in a great bar. Probably not the right place to practice, but if you play well (especially Nordstrom-style music), and the regular pianist is not there, the staff would welcome you playing
', N'<p>A nice piano in a great bar. Probably not the right place to practice, but if you play well (especially Nordstrom-style music), and the regular pianist is not there, the staff would welcome you playing</p>
', 43)
INSERT [dbo].[InstrumentReviewRevisions] ([RevisionID], [ReviewID], [UserID], [RevisionDate], [RatingGeneral], [LastUseDate], [MessageMarkdown], [MessageHTML], [GlobalPostID]) VALUES (14, 12, N'4c55c04f-6fe7-442e-84de-e4c43803a666', CAST(0x00009FC800AD62EF AS DateTime), 1, NULL, N'I encountered this instrument when descending from the Sequoia Park along the Kings Canyon Road. It is on terrace of the Lodge, and appears a pretty beaten up but playable piano (if you are content on playing outside, and someone would hold the lid for you). Quite out of tune, and sounds a bit like clavesine, but if you are desperate to play after several days in the mountains -this is an option. No people where in sight, so nobody to ask if it was Ok to play.', N'<p>I encountered this instrument when descending from the Sequoia Park along the Kings Canyon Road. It is on terrace of the Lodge, and appears a pretty beaten up but playable piano (if you are content on playing outside, and someone would hold the lid for you). Quite out of tune, and sounds a bit like clavesine, but if you are desperate to play after several days in the mountains -this is an option. No people where in sight, so nobody to ask if it was Ok to play.</p>
', 46)
SET IDENTITY_INSERT [dbo].[InstrumentReviewRevisions] OFF
/****** Object:  Table [dbo].[Messages]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[ConversationID] [int] NOT NULL,
	[SenderID] [uniqueidentifier] NOT NULL,
	[ReceipientID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Markdown] [nvarchar](max) NOT NULL,
	[Html] [nvarchar](max) NOT NULL,
	[NumberInConvo] [int] NOT NULL,
	[IsUnread] [bit] NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Messages] ON [dbo].[Messages] 
(
	[ConversationID] ASC,
	[NumberInConvo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Messages] ON
INSERT [dbo].[Messages] ([MessageID], [ConversationID], [SenderID], [ReceipientID], [Date], [Markdown], [Html], [NumberInConvo], [IsUnread], [GlobalPostID]) VALUES (1, 1, N'fc376fa1-7667-4f24-848b-18e7f084805e', N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FC200BE84D7 AS DateTime), N'Hey,

I *really hate* your site. Why can''t you make a normal thing, dammit?

-z', N'<p>Hey,</p>

<p>I <em>really hate</em> your site. Why can''t you make a normal thing, dammit?</p>

<p>-z</p>
', 1, 1, 34)
INSERT [dbo].[Messages] ([MessageID], [ConversationID], [SenderID], [ReceipientID], [Date], [Markdown], [Html], [NumberInConvo], [IsUnread], [GlobalPostID]) VALUES (2, 1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'fc376fa1-7667-4f24-848b-18e7f084805e', CAST(0x00009FC200BEA9B4 AS DateTime), N'idk..', N'<p>idk..</p>
', 2, 1, 35)
INSERT [dbo].[Messages] ([MessageID], [ConversationID], [SenderID], [ReceipientID], [Date], [Markdown], [Html], [NumberInConvo], [IsUnread], [GlobalPostID]) VALUES (3, 2, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC7014FF3AB AS DateTime), N'Test!', N'<p>Test!</p>
', 1, 1, 39)
INSERT [dbo].[Messages] ([MessageID], [ConversationID], [SenderID], [ReceipientID], [Date], [Markdown], [Html], [NumberInConvo], [IsUnread], [GlobalPostID]) VALUES (4, 2, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC701503EB2 AS DateTime), N'Another test!', N'<p>Another test!</p>
', 2, 1, 40)
SET IDENTITY_INSERT [dbo].[Messages] OFF
/****** Object:  Table [dbo].[MessageFlags]    Script Date: 06/24/2012 15:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageFlags](
	[FlagID] [int] IDENTITY(1,1) NOT NULL,
	[MessageID] [int] NOT NULL,
	[FlaggerID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[ModResponse] [bit] NULL,
 CONSTRAINT [PK_MessageFlags] PRIMARY KEY CLUSTERED 
(
	[FlagID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MessageFlags] ON
INSERT [dbo].[MessageFlags] ([FlagID], [MessageID], [FlaggerID], [Date], [ModResponse]) VALUES (1, 1, N'63887c64-e7cc-41d0-8a14-fd6327ec52c2', CAST(0x00009FC70156C9B6 AS DateTime), NULL)
INSERT [dbo].[MessageFlags] ([FlagID], [MessageID], [FlaggerID], [Date], [ModResponse]) VALUES (4, 3, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC8001CCA4F AS DateTime), NULL)
INSERT [dbo].[MessageFlags] ([FlagID], [MessageID], [FlaggerID], [Date], [ModResponse]) VALUES (5, 4, N'b1e94788-876a-4b8f-9860-0534fc521d7b', CAST(0x00009FC8001CD2C2 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[MessageFlags] OFF
/****** Object:  Default [DF__aspnet_Ap__Appli__06CD04F7]    Script Date: 06/24/2012 15:04:01 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD  DEFAULT (newid()) FOR [ApplicationId]
GO
/****** Object:  Default [DF__aspnet_Us__UserI__09A971A2]    Script Date: 06/24/2012 15:04:03 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (newid()) FOR [UserId]
GO
/****** Object:  Default [DF__aspnet_Us__Mobil__0A9D95DB]    Script Date: 06/24/2012 15:04:03 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (NULL) FOR [MobileAlias]
GO
/****** Object:  Default [DF__aspnet_Us__IsAno__0B91BA14]    Script Date: 06/24/2012 15:04:03 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT ((0)) FOR [IsAnonymous]
GO
/****** Object:  Default [DF__aspnet_Pa__PathI__07C12930]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Paths] ADD  DEFAULT (newid()) FOR [PathId]
GO
/****** Object:  Default [DF__aspnet_Ro__RoleI__08B54D69]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Roles] ADD  DEFAULT (newid()) FOR [RoleId]
GO
/****** Object:  Default [DF__aspnet_Perso__Id__0D7A0286]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF__aspnet_Me__Passw__0E6E26BF]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Membership] ADD  DEFAULT ((0)) FOR [PasswordFormat]
GO
/****** Object:  Default [DF_Instruments_ListingViews]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Instruments] ADD  CONSTRAINT [DF_Instruments_ListingViews]  DEFAULT ((0)) FOR [ListingViews]
GO
/****** Object:  Default [DF_Instruments_AddressPrivacy]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Instruments] ADD  CONSTRAINT [DF_Instruments_AddressPrivacy]  DEFAULT ((0)) FOR [AddressPrivacy]
GO
/****** Object:  ForeignKey [FK__aspnet_Us__Appli__114A936A]    Script Date: 06/24/2012 15:04:03 ******/
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pa__Appli__0F624AF8]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Ro__Appli__10566F31]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__PathI__18EBB532]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__UserI__19DFD96B]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pr__UserI__1AD3FDA4]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Me__Appli__1BC821DD]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Me__UserI__1CBC4616]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Pe__PathI__1DB06A4F]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
/****** Object:  ForeignKey [FK_Instruments_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Instruments]  WITH CHECK ADD  CONSTRAINT [FK_Instruments_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Instruments] CHECK CONSTRAINT [FK_Instruments_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_Instruments_InstrumentTypes]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Instruments]  WITH CHECK ADD  CONSTRAINT [FK_Instruments_InstrumentTypes] FOREIGN KEY([TypeID])
REFERENCES [dbo].[InstrumentTypes] ([TypeID])
GO
ALTER TABLE [dbo].[Instruments] CHECK CONSTRAINT [FK_Instruments_InstrumentTypes]
GO
/****** Object:  ForeignKey [FK_GlobalPostIDs_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[GlobalPostIDs]  WITH CHECK ADD  CONSTRAINT [FK_GlobalPostIDs_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[GlobalPostIDs] CHECK CONSTRAINT [FK_GlobalPostIDs_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_PianoUserSuspensions_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[UserSuspensions]  WITH CHECK ADD  CONSTRAINT [FK_PianoUserSuspensions_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSuspensions] CHECK CONSTRAINT [FK_PianoUserSuspensions_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_UserOpenIds_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[UserOpenIds]  WITH CHECK ADD  CONSTRAINT [FK_UserOpenIds_aspnet_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[UserOpenIds] CHECK CONSTRAINT [FK_UserOpenIds_aspnet_Users]
GO
/****** Object:  ForeignKey [FK__aspnet_Us__RoleI__17036CC0]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__UserI__17F790F9]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK_Votes_GlobalPostIDs]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_GlobalPostIDs] FOREIGN KEY([PostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_GlobalPostIDs]
GO
/****** Object:  ForeignKey [FK_Votes_VoteTypes]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_VoteTypes] FOREIGN KEY([VoteType])
REFERENCES [dbo].[VoteTypes] ([VoteTypeID])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_VoteTypes]
GO
/****** Object:  ForeignKey [VotedUponUser]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [VotedUponUser] FOREIGN KEY([UserAffectedByVoteID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [VotedUponUser]
GO
/****** Object:  ForeignKey [Voter]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [Voter] FOREIGN KEY([VoterID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [Voter]
GO
/****** Object:  ForeignKey [FK_ResetPasswordRecords_aspnet_Membership]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[ResetPasswordRecords]  WITH CHECK ADD  CONSTRAINT [FK_ResetPasswordRecords_aspnet_Membership] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Membership] ([UserId])
GO
ALTER TABLE [dbo].[ResetPasswordRecords] CHECK CONSTRAINT [FK_ResetPasswordRecords_aspnet_Membership]
GO
/****** Object:  ForeignKey [FK_Notifications_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_Notifications_GlobalPostIDs]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_GlobalPostIDs]
GO
/****** Object:  ForeignKey [FK_InstrumentHours_Instruments]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[InstrumentHours]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentHours_Instruments] FOREIGN KEY([InstrumentID])
REFERENCES [dbo].[Instruments] ([InstrumentID])
GO
ALTER TABLE [dbo].[InstrumentHours] CHECK CONSTRAINT [FK_InstrumentHours_Instruments]
GO
/****** Object:  ForeignKey [FK_Conversations_aspnet_Users_1]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_aspnet_Users_1] FOREIGN KEY([User1])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_aspnet_Users_1]
GO
/****** Object:  ForeignKey [FK_Conversations_aspnet_Users_2]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_aspnet_Users_2] FOREIGN KEY([User2])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_aspnet_Users_2]
GO
/****** Object:  ForeignKey [FK_Conversations_GlobalPostIDs]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_GlobalPostIDs]
GO
/****** Object:  ForeignKey [FK_ConfirmEmailAddresses_aspnet_Membership]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[ConfirmEmailAddresses]  WITH CHECK ADD  CONSTRAINT [FK_ConfirmEmailAddresses_aspnet_Membership] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Membership] ([UserId])
GO
ALTER TABLE [dbo].[ConfirmEmailAddresses] CHECK CONSTRAINT [FK_ConfirmEmailAddresses_aspnet_Membership]
GO
/****** Object:  ForeignKey [FK_InstrumentReviews_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[InstrumentReviews]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentReviews_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[InstrumentReviews] CHECK CONSTRAINT [FK_InstrumentReviews_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_InstrumentReviews_Instruments]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[InstrumentReviews]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentReviews_Instruments] FOREIGN KEY([InstrumentID])
REFERENCES [dbo].[Instruments] ([InstrumentID])
GO
ALTER TABLE [dbo].[InstrumentReviews] CHECK CONSTRAINT [FK_InstrumentReviews_Instruments]
GO
/****** Object:  ForeignKey [FK_InstrumentReviewRevisions_aspnet_Users]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[InstrumentReviewRevisions]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentReviewRevisions_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[InstrumentReviewRevisions] CHECK CONSTRAINT [FK_InstrumentReviewRevisions_aspnet_Users]
GO
/****** Object:  ForeignKey [FK_InstrumentReviewRevisions_InstrumentReviews]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[InstrumentReviewRevisions]  WITH CHECK ADD  CONSTRAINT [FK_InstrumentReviewRevisions_InstrumentReviews] FOREIGN KEY([ReviewID])
REFERENCES [dbo].[InstrumentReviews] ([ReviewID])
GO
ALTER TABLE [dbo].[InstrumentReviewRevisions] CHECK CONSTRAINT [FK_InstrumentReviewRevisions_InstrumentReviews]
GO
/****** Object:  ForeignKey [FK_Messages_aspnet_Users_1]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_aspnet_Users_1] FOREIGN KEY([SenderID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_aspnet_Users_1]
GO
/****** Object:  ForeignKey [FK_Messages_aspnet_Users_2]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_aspnet_Users_2] FOREIGN KEY([ReceipientID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_aspnet_Users_2]
GO
/****** Object:  ForeignKey [FK_Messages_Conversations]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Conversations] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[Conversations] ([ConversationID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Conversations]
GO
/****** Object:  ForeignKey [FK_Messages_GlobalPostIDs]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_GlobalPostIDs]
GO
/****** Object:  ForeignKey [FK_MessageFlags_MessageFlags1]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[MessageFlags]  WITH CHECK ADD  CONSTRAINT [FK_MessageFlags_MessageFlags1] FOREIGN KEY([FlaggerID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[MessageFlags] CHECK CONSTRAINT [FK_MessageFlags_MessageFlags1]
GO
/****** Object:  ForeignKey [FK_MessageFlags_Messages]    Script Date: 06/24/2012 15:04:04 ******/
ALTER TABLE [dbo].[MessageFlags]  WITH CHECK ADD  CONSTRAINT [FK_MessageFlags_Messages] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Messages] ([MessageID])
GO
ALTER TABLE [dbo].[MessageFlags] CHECK CONSTRAINT [FK_MessageFlags_Messages]
GO
