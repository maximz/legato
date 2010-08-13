CREATE TABLE [dbo].[DiscussBoards] (
    [BoardID]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [BoardName]   NVARCHAR (100) NOT NULL,
    [IsCityBoard] BIT            NOT NULL
);

