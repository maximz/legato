CREATE TABLE [dbo].[InstrumentTypes] (
    [TypeID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [InstrumentID] BIGINT        NOT NULL,
    [TypeName]     NVARCHAR (50) NOT NULL
);

