CREATE TABLE [dbo].[DiscussPostFlags] (
    [FlagID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [PostID]   BIGINT           NOT NULL,
    [TypeID]   INT              NOT NULL,
    [UserID]   UNIQUEIDENTIFIER NOT NULL,
    [FlagDate] DATETIME         NOT NULL
);

