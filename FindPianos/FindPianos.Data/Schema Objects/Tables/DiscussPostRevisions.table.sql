CREATE TABLE [dbo].[DiscussPostRevisions] (
    [PostRevisionID]  BIGINT           IDENTITY (1, 1) NOT NULL,
    [UserID]          UNIQUEIDENTIFIER NOT NULL,
    [PostID]          BIGINT           NOT NULL,
    [Markdown]        NVARCHAR (MAX)   NOT NULL,
    [HTML]            NVARCHAR (MAX)   NOT NULL,
    [DateOfEdit]      DATETIME         NOT NULL,
    [EditNumber]      INT              NOT NULL,
    [InReplyToPostID] BIGINT           NULL
);

