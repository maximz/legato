CREATE TABLE [dbo].[SaleReviewRevisions] (
    [SaleReviewRevisionID]                   BIGINT           IDENTITY (1, 1) NOT NULL,
    [SaleReviewID]                           BIGINT           NOT NULL,
    [RatingOverall]                          INT              NOT NULL,
    [RatingTuning]                           INT              NULL,
    [RatingToneQuality]                      INT              NULL,
    [RatingPlayingCapability]                INT              NULL,
    [Message]                                NVARCHAR (MAX)   NULL,
    [SubmitterUserID]                        UNIQUEIDENTIFIER NOT NULL,
    [DateOfRevision]                         DATETIME         NOT NULL,
    [DateOfLastUsageOfInstrumentBySubmitter] DATETIME         NOT NULL,
    [RevisionNumberOfReview]                 INT              NOT NULL,
    [IsSubmitterAffiliatedWithSeller]        BIT              NOT NULL
);

