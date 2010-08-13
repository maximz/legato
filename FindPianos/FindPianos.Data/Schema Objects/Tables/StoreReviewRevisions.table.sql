CREATE TABLE [dbo].[StoreReviewRevisions] (
    [StoreReviewRevisionID] BIGINT           IDENTITY (1, 1) NOT NULL,
    [ReviewID]              BIGINT           NOT NULL,
    [RatingOverall]         INT              NOT NULL,
    [RatingService]         INT              NULL,
    [RatingProductQuality]  INT              NULL,
    [RatingEnvironment]     INT              NULL,
    [DateOfLastVisit]       DATETIME         NOT NULL,
    [DateOfLastPurchase]    DATETIME         NOT NULL,
    [Message]               NVARCHAR (MAX)   NULL,
    [RevisionDate]          DATETIME         NOT NULL,
    [EditNumber]            INT              NOT NULL,
    [UserID]                UNIQUEIDENTIFIER NOT NULL
);

