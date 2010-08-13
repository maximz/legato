CREATE TABLE [dbo].[SaleListings] (
    [SaleListingID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [InstrumentID]            BIGINT           NOT NULL,
    [Price]                   FLOAT            NOT NULL,
    [Lat]                     DECIMAL (18)     NOT NULL,
    [Long]                    DECIMAL (18)     NOT NULL,
    [StreetAddress]           NVARCHAR (MAX)   NOT NULL,
    [OriginalSubmitterUserID] UNIQUEIDENTIFIER NOT NULL,
    [DateOfSubmission]        DATETIME         NOT NULL,
    [InstrumentBrand]         NVARCHAR (50)    NOT NULL,
    [InstrumentModel]         NVARCHAR (50)    NULL,
    [InstrumentTypeID]        BIGINT           NOT NULL,
    [InstrumentStyleID]       BIGINT           NOT NULL,
    [SoldByStoreID]           BIGINT           NULL,
    [IsFiled]                 BIT              NOT NULL,
    [FiledStartDate]          DATETIME         NULL,
    [FiledEndDate]            DATETIME         NULL
);

