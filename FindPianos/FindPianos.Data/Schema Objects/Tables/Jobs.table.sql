CREATE TABLE [dbo].[Jobs] (
    [JobID]                BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserID]               UNIQUEIDENTIFIER NOT NULL,
    [StreetAddress]        NVARCHAR (MAX)   NULL,
    [Lat]                  DECIMAL (18)     NULL,
    [Long]                 DECIMAL (18)     NULL,
    [DescriptionMarkdown]  NVARCHAR (MAX)   NOT NULL,
    [RequirementsMarkdown] NVARCHAR (MAX)   NOT NULL,
    [OtherMarkdown]        NVARCHAR (MAX)   NULL,
    [DescriptionHTML]      NVARCHAR (MAX)   NOT NULL,
    [RequirementsHTML]     NVARCHAR (MAX)   NOT NULL,
    [OtherHTML]            NVARCHAR (MAX)   NULL
);

