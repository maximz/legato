CREATE TABLE [dbo].[Resumes] (
    [ResumeID]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [ResumeTypeID]         INT              NOT NULL,
    [UserID]               UNIQUEIDENTIFIER NOT NULL,
    [IsFiled]              BIT              NOT NULL,
    [FiledStartDate]       DATETIME         NULL,
    [FiledEndDate]         DATETIME         NULL,
    [Name]                 NVARCHAR (MAX)   NOT NULL,
    [AboutMarkdown]        NVARCHAR (MAX)   NOT NULL,
    [AchievementsMarkdown] NVARCHAR (MAX)   NOT NULL,
    [EducationMarkdown]    NVARCHAR (MAX)   NOT NULL,
    [ExperienceMarkdown]   NVARCHAR (MAX)   NOT NULL,
    [OtherMarkdown]        NVARCHAR (MAX)   NULL,
    [AboutHTML]            NVARCHAR (MAX)   NOT NULL,
    [AchievementsHTML]     NVARCHAR (MAX)   NOT NULL,
    [EducationHTML]        NVARCHAR (MAX)   NOT NULL,
    [ExperienceHTML]       NVARCHAR (MAX)   NOT NULL,
    [OtherHTML]            NVARCHAR (MAX)   NULL
);

