CREATE TABLE [dbo].[ReviewRevisions] (
    [ReviewRevisionID]                  BIGINT           IDENTITY (1, 1) NOT NULL,
    [ReviewID]                          BIGINT           NOT NULL,
    [RatingOverall]                     INT              NOT NULL,
    [RatingTuning]                      INT              NULL,
    [RatingToneQuality]                 INT              NULL,
    [RatingPlayingCapability]           INT              NULL,
    [Message]                           NVARCHAR (MAX)   NULL,
    [PricePerHourInUSD]                 FLOAT            NOT NULL,
    [VenueName]                         NVARCHAR (MAX)   NOT NULL,
    [SubmitterUserID]                   UNIQUEIDENTIFIER NOT NULL,
    [DateOfRevision]                    DATETIME         NOT NULL,
    [DateOfLastUsageOfPianoBySubmitter] DATETIME         NOT NULL,
    [RevisionNumberOfReview]            INT              NOT NULL
);

