USE [master]
GO
/****** Object:  Database [FindPianosToilets]    Script Date: 06/07/2010 18:13:28 ******/
CREATE DATABASE [FindPianosToilets] ON  PRIMARY 
( NAME = N'FindPianos', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER2008\MSSQL\DATA\FindPianos.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FindPianos_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER2008\MSSQL\DATA\FindPianos_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FindPianosToilets] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FindPianosToilets].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FindPianosToilets] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FindPianosToilets] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FindPianosToilets] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FindPianosToilets] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FindPianosToilets] SET ARITHABORT OFF
GO
ALTER DATABASE [FindPianosToilets] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FindPianosToilets] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FindPianosToilets] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FindPianosToilets] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FindPianosToilets] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FindPianosToilets] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FindPianosToilets] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FindPianosToilets] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FindPianosToilets] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FindPianosToilets] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FindPianosToilets] SET  DISABLE_BROKER
GO
ALTER DATABASE [FindPianosToilets] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FindPianosToilets] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FindPianosToilets] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FindPianosToilets] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FindPianosToilets] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FindPianosToilets] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FindPianosToilets] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FindPianosToilets] SET  READ_WRITE
GO
ALTER DATABASE [FindPianosToilets] SET RECOVERY FULL
GO
ALTER DATABASE [FindPianosToilets] SET  MULTI_USER
GO
ALTER DATABASE [FindPianosToilets] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FindPianosToilets] SET DB_CHAINING OFF
GO
USE [FindPianosToilets]
GO
/****** Object:  User [pianoapp]    Script Date: 06/07/2010 18:13:28 ******/
CREATE USER [pianotoiletapp] FOR LOGIN [pianotoiletapp] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[PianoListings]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoListings](
	[PianoID] [bigint] IDENTITY(1,1) NOT NULL,
	[Lat] [decimal](18, 0) NOT NULL,
	[Long] [decimal](18, 0) NOT NULL,
	[StreetAddress] [nvarchar](max) NULL,
	[OriginalSubmitterUserID] [bigint] NOT NULL,
	[DateOfSubmission] [datetime] NOT NULL,
 CONSTRAINT [PK_PianoListing] PRIMARY KEY CLUSTERED 
(
	[PianoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoStyles]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoStyles](
	[PianoStyleID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PianoStyles] PRIMARY KEY CLUSTERED 
(
	[PianoStyleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToiletListings]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToiletListings](
	[ToiletID] [bigint] IDENTITY(1,1) NOT NULL,
	[Lat] [decimal](18, 0) NOT NULL,
	[Long] [decimal](18, 0) NOT NULL,
	[StreetAddress] [nvarchar](max) NULL,
	[OriginalSubmitterUserID] [bigint] NOT NULL,
	[DateOfSubmission] [datetime] NOT NULL,
 CONSTRAINT [PK_ToiletListing] PRIMARY KEY CLUSTERED 
(
	[ToiletID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoVenues]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoVenues](
	[VenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[VenueName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PianoVenues] PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekDays]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekDays](
	[WeekDayID] [int] NOT NULL,
	[WeekDayName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_WeekDays] PRIMARY KEY CLUSTERED 
(
	[WeekDayID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToiletVenues]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToiletVenues](
	[VenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[VenueName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ToiletVenues] PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToiletStyles]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToiletStyles](
	[ToiletStyleID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ToiletStyles] PRIMARY KEY CLUSTERED 
(
	[ToiletStyleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToiletReviews]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToiletReviews](
	[ToiletReviewID] [bigint] IDENTITY(1,1) NOT NULL,
	[ToiletListingID] [bigint] NOT NULL,
	[ToiletStyleID] [int] NOT NULL,
	[UrinalCount] [int] NULL,
	[StallCount] [int] NULL,
	[SinkCount] [int] NULL,
	[BabyChangingStationCount] [int] NULL,
	[RatingOverall] [int] NOT NULL,
	[RatingCleanliness] [int] NULL,
	[RatingAvailability] [int] NULL,
	[TypicalWait] [float] NULL,
	[Message] [nvarchar](max) NULL,
	[VenueID] [bigint] NOT NULL,
	[DateOfSubmission] [datetime] NOT NULL,
	[SubmitterUserID] [bigint] NOT NULL,
	[DateOfLastUsageOfToiletBySubmitter] [datetime] NOT NULL,
 CONSTRAINT [PK_ToiletReviews] PRIMARY KEY CLUSTERED 
(
	[ToiletReviewID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToiletVenueHours]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToiletVenueHours](
	[VenueHoursID] [bigint] IDENTITY(1,1) NOT NULL,
	[VenueID] [bigint] NOT NULL,
	[DayOfWeek] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ToiletVenueHours] PRIMARY KEY CLUSTERED 
(
	[VenueHoursID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoVenueHours]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoVenueHours](
	[VenueHoursID] [bigint] IDENTITY(1,1) NOT NULL,
	[VenueID] [bigint] NOT NULL,
	[DayOfWeek] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PianoVenueHours] PRIMARY KEY CLUSTERED 
(
	[VenueHoursID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoReviews]    Script Date: 06/07/2010 18:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoReviews](
	[PianoReviewID] [bigint] IDENTITY(1,1) NOT NULL,
	[PianoListingID] [bigint] NOT NULL,
	[PianoStyleID] [int] NOT NULL,
	[Brand] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[RatingOverall] [int] NOT NULL,
	[RatingTuning] [int] NULL,
	[RatingToneQuality] [int] NULL,
	[RatingPlayingCapability] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[VenueID] [bigint] NOT NULL,
	[DateOfSubmission] [datetime] NOT NULL,
	[SubmitterUserID] [bigint] NOT NULL,
	[DateOfLastUsageOfPianoBySubmitter] [datetime] NOT NULL,
 CONSTRAINT [PK_PianoReviews] PRIMARY KEY CLUSTERED 
(
	[PianoReviewID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletListings]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletListings] FOREIGN KEY([ToiletListingID])
REFERENCES [dbo].[ToiletListings] ([ToiletID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletListings]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletStyles]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletStyles] FOREIGN KEY([ToiletStyleID])
REFERENCES [dbo].[ToiletStyles] ([ToiletStyleID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletStyles]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletVenues]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[ToiletVenues] ([VenueID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletVenues]
GO
/****** Object:  ForeignKey [FK_ToiletVenueHours_ToiletVenues]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[ToiletVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_ToiletVenueHours_ToiletVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[ToiletVenues] ([VenueID])
GO
ALTER TABLE [dbo].[ToiletVenueHours] CHECK CONSTRAINT [FK_ToiletVenueHours_ToiletVenues]
GO
/****** Object:  ForeignKey [FK_ToiletVenueHours_WeekDays]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[ToiletVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_ToiletVenueHours_WeekDays] FOREIGN KEY([DayOfWeek])
REFERENCES [dbo].[WeekDays] ([WeekDayID])
GO
ALTER TABLE [dbo].[ToiletVenueHours] CHECK CONSTRAINT [FK_ToiletVenueHours_WeekDays]
GO
/****** Object:  ForeignKey [FK_PianoVenueHours_PianoVenues]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[PianoVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_PianoVenueHours_PianoVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[PianoVenues] ([VenueID])
GO
ALTER TABLE [dbo].[PianoVenueHours] CHECK CONSTRAINT [FK_PianoVenueHours_PianoVenues]
GO
/****** Object:  ForeignKey [FK_PianoVenueHours_WeekDays]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[PianoVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_PianoVenueHours_WeekDays] FOREIGN KEY([DayOfWeek])
REFERENCES [dbo].[WeekDays] ([WeekDayID])
GO
ALTER TABLE [dbo].[PianoVenueHours] CHECK CONSTRAINT [FK_PianoVenueHours_WeekDays]
GO
/****** Object:  ForeignKey [FK_PianoReviews_PianoListings]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[PianoReviews]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviews_PianoListings] FOREIGN KEY([PianoListingID])
REFERENCES [dbo].[PianoListings] ([PianoID])
GO
ALTER TABLE [dbo].[PianoReviews] CHECK CONSTRAINT [FK_PianoReviews_PianoListings]
GO
/****** Object:  ForeignKey [FK_PianoReviews_PianoStyles]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[PianoReviews]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviews_PianoStyles] FOREIGN KEY([PianoStyleID])
REFERENCES [dbo].[PianoStyles] ([PianoStyleID])
GO
ALTER TABLE [dbo].[PianoReviews] CHECK CONSTRAINT [FK_PianoReviews_PianoStyles]
GO
/****** Object:  ForeignKey [FK_PianoReviews_PianoVenues]    Script Date: 06/07/2010 18:13:31 ******/
ALTER TABLE [dbo].[PianoReviews]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviews_PianoVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[PianoVenues] ([VenueID])
GO
ALTER TABLE [dbo].[PianoReviews] CHECK CONSTRAINT [FK_PianoReviews_PianoVenues]
GO
