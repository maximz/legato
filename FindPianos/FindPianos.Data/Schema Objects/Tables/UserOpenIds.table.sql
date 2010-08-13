CREATE TABLE [dbo].[UserOpenIds] (
    [Id]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [OpenIdClaim] NVARCHAR (450)   NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL
);

