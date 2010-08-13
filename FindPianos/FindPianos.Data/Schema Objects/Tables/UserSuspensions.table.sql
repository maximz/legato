CREATE TABLE [dbo].[UserSuspensions] (
    [SuspensionID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserID]         UNIQUEIDENTIFIER NOT NULL,
    [Reason]         NVARCHAR (MAX)   NULL,
    [SuspensionDate] DATETIME         NOT NULL,
    [ReinstateDate]  DATETIME         NOT NULL
);

