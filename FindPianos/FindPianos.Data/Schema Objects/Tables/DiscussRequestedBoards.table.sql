CREATE TABLE [dbo].[DiscussRequestedBoards] (
    [RequestedBoardID] INT              IDENTITY (1, 1) NOT NULL,
    [BoardName]        NVARCHAR (100)   NOT NULL,
    [RequestDate]      DATETIME         NOT NULL,
    [RequestUserID]    UNIQUEIDENTIFIER NOT NULL
);

