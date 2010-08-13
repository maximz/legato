CREATE TABLE [dbo].[InstrumentStyles] (
    [StyleID]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [InstrumentID] BIGINT        NOT NULL,
    [StyleName]    NVARCHAR (50) NOT NULL
);

