CREATE TABLE [dbo].[StoreListings] (
    [StoreListingID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [Address]                        NVARCHAR (MAX)   NOT NULL,
    [Lat]                            DECIMAL (18)     NOT NULL,
    [Long]                           DECIMAL (18)     NOT NULL,
    [Name]                           NVARCHAR (MAX)   NOT NULL,
    [Description]                    NVARCHAR (MAX)   NOT NULL,
    [SubmitterUserID]                UNIQUEIDENTIFIER NOT NULL,
    [IsSubmitterAffiliatedWithStore] BIT              NOT NULL,
    [DateOfSubmission]               DATETIME         NOT NULL
);

