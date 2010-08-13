CREATE TABLE [dbo].[ReviewFlags] (
    [FlagID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [ReviewID] BIGINT           NOT NULL,
    [TypeID]   INT              NOT NULL,
    [UserID]   UNIQUEIDENTIFIER NOT NULL,
    [FlagDate] DATETIME         NOT NULL
);

