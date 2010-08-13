CREATE TABLE [dbo].[DiscussThreads] (
    [ThreadID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [BoardID]        BIGINT         NOT NULL,
    [Title]          NVARCHAR (100) NOT NULL,
    [CreationDate]   DATETIME       NOT NULL,
    [Latitude]       DECIMAL (18)   NULL,
    [Longitude]      DECIMAL (18)   NULL,
    [Address]        NVARCHAR (MAX) NULL,
    [LatestActivity] DATETIME       NOT NULL
);

