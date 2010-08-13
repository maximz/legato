CREATE TABLE [dbo].[DiscussPosts] (
    [PostID]             BIGINT   IDENTITY (1, 1) NOT NULL,
    [ThreadID]           BIGINT   NOT NULL,
    [PostNumberInThread] INT      NOT NULL,
    [DateOfSubmission]   DATETIME NOT NULL,
    [LatestActivity]     DATETIME NOT NULL
);

