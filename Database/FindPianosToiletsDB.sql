USE [FindPianosToilets]
GO
/****** Object:  User [pianotoiletapp]    Script Date: 06/16/2010 00:10:00 ******/
CREATE USER [pianotoiletapp] FOR LOGIN [pianotoiletapp] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[PianoStyles]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[ToiletListings]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[PianoVenues]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[PianoListings]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[ToiletStyles]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[WeekDays]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[ToiletVenues]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[ToiletVenueHours]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[ToiletReviews]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[PianoVenueHours]    Script Date: 06/16/2010 00:10:04 ******/
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
/****** Object:  Table [dbo].[PianoReviews]    Script Date: 06/16/2010 00:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoReviews](
	[PianoReviewID] [bigint] IDENTITY(1,1) NOT NULL,
	[PianoListingID] [bigint] NOT NULL,
 CONSTRAINT [PK_PianoReviews] PRIMARY KEY CLUSTERED 
(
	[PianoReviewID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoReviewRevisions]    Script Date: 06/16/2010 00:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoReviewRevisions](
	[PianoReviewRevisionID] [bigint] IDENTITY(1,1) NOT NULL,
	[PianoReviewID] [bigint] NOT NULL,
	[PianoStyleID] [int] NOT NULL,
	[Brand] [nvarchar](max) NULL,
	[Model] [nvarchar](max) NULL,
	[RatingOverall] [int] NOT NULL,
	[RatingTuning] [int] NULL,
	[RatingToneQuality] [int] NULL,
	[RatingPlayingCapability] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[VenueID] [bigint] NOT NULL,
	[DateOfRevision] [datetime] NOT NULL,
	[SubmitterUserID] [bigint] NOT NULL,
	[DateOfLastUsageOfPianoBySubmitter] [datetime] NOT NULL,
	[RevisionNumberOfReview] [int] NOT NULL,
 CONSTRAINT [PK_PianoReviewRevisions] PRIMARY KEY CLUSTERED 
(
	[PianoReviewRevisionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PianoReviewComments]    Script Date: 06/16/2010 00:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PianoReviewComments](
	[CommentID] [bigint] IDENTITY(1,1) NOT NULL,
	[PianoReviewID] [bigint] NOT NULL,
	[AuthorUserID] [bigint] NOT NULL,
	[MessageMarkdown] [nvarchar](max) NOT NULL,
	[MessageHTML] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PianoReviewComments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_ToiletVenueHours_ToiletVenues]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[ToiletVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_ToiletVenueHours_ToiletVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[ToiletVenues] ([VenueID])
GO
ALTER TABLE [dbo].[ToiletVenueHours] CHECK CONSTRAINT [FK_ToiletVenueHours_ToiletVenues]
GO
/****** Object:  ForeignKey [FK_ToiletVenueHours_WeekDays]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[ToiletVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_ToiletVenueHours_WeekDays] FOREIGN KEY([DayOfWeek])
REFERENCES [dbo].[WeekDays] ([WeekDayID])
GO
ALTER TABLE [dbo].[ToiletVenueHours] CHECK CONSTRAINT [FK_ToiletVenueHours_WeekDays]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletListings]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletListings] FOREIGN KEY([ToiletListingID])
REFERENCES [dbo].[ToiletListings] ([ToiletID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletListings]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletStyles]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletStyles] FOREIGN KEY([ToiletStyleID])
REFERENCES [dbo].[ToiletStyles] ([ToiletStyleID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletStyles]
GO
/****** Object:  ForeignKey [FK_ToiletReviews_ToiletVenues]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[ToiletReviews]  WITH CHECK ADD  CONSTRAINT [FK_ToiletReviews_ToiletVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[ToiletVenues] ([VenueID])
GO
ALTER TABLE [dbo].[ToiletReviews] CHECK CONSTRAINT [FK_ToiletReviews_ToiletVenues]
GO
/****** Object:  ForeignKey [FK_PianoVenueHours_PianoVenues]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_PianoVenueHours_PianoVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[PianoVenues] ([VenueID])
GO
ALTER TABLE [dbo].[PianoVenueHours] CHECK CONSTRAINT [FK_PianoVenueHours_PianoVenues]
GO
/****** Object:  ForeignKey [FK_PianoVenueHours_WeekDays]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoVenueHours]  WITH CHECK ADD  CONSTRAINT [FK_PianoVenueHours_WeekDays] FOREIGN KEY([DayOfWeek])
REFERENCES [dbo].[WeekDays] ([WeekDayID])
GO
ALTER TABLE [dbo].[PianoVenueHours] CHECK CONSTRAINT [FK_PianoVenueHours_WeekDays]
GO
/****** Object:  ForeignKey [FK_PianoReviews_PianoListings]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoReviews]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviews_PianoListings] FOREIGN KEY([PianoListingID])
REFERENCES [dbo].[PianoListings] ([PianoID])
GO
ALTER TABLE [dbo].[PianoReviews] CHECK CONSTRAINT [FK_PianoReviews_PianoListings]
GO
/****** Object:  ForeignKey [FK_PianoReviewRevisions_PianoReviews]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoReviewRevisions]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviewRevisions_PianoReviews] FOREIGN KEY([PianoReviewID])
REFERENCES [dbo].[PianoReviews] ([PianoReviewID])
GO
ALTER TABLE [dbo].[PianoReviewRevisions] CHECK CONSTRAINT [FK_PianoReviewRevisions_PianoReviews]
GO
/****** Object:  ForeignKey [FK_PianoReviewRevisions_PianoStyles]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoReviewRevisions]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviewRevisions_PianoStyles] FOREIGN KEY([PianoStyleID])
REFERENCES [dbo].[PianoStyles] ([PianoStyleID])
GO
ALTER TABLE [dbo].[PianoReviewRevisions] CHECK CONSTRAINT [FK_PianoReviewRevisions_PianoStyles]
GO
/****** Object:  ForeignKey [FK_PianoReviewRevisions_PianoVenues]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoReviewRevisions]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviewRevisions_PianoVenues] FOREIGN KEY([VenueID])
REFERENCES [dbo].[PianoVenues] ([VenueID])
GO
ALTER TABLE [dbo].[PianoReviewRevisions] CHECK CONSTRAINT [FK_PianoReviewRevisions_PianoVenues]
GO
/****** Object:  ForeignKey [FK_PianoReviewComments_PianoReviews]    Script Date: 06/16/2010 00:10:04 ******/
ALTER TABLE [dbo].[PianoReviewComments]  WITH CHECK ADD  CONSTRAINT [FK_PianoReviewComments_PianoReviews] FOREIGN KEY([PianoReviewID])
REFERENCES [dbo].[PianoReviews] ([PianoReviewID])
GO
ALTER TABLE [dbo].[PianoReviewComments] CHECK CONSTRAINT [FK_PianoReviewComments_PianoReviews]
GO
