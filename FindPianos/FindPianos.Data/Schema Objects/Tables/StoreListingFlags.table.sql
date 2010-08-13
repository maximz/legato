CREATE TABLE [dbo].[StoreListingFlags] (
    [FlagID]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [ListingID] BIGINT           NOT NULL,
    [TypeID]    INT              NOT NULL,
    [UserID]    UNIQUEIDENTIFIER NOT NULL,
    [FlagDate]  DATETIME         NOT NULL
);

