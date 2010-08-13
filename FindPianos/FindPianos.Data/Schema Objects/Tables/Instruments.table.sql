CREATE TABLE [dbo].[Instruments] (
    [InstrumentID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (50) NOT NULL,
    [IsIncludedInTemporary] BIT           NOT NULL
);

