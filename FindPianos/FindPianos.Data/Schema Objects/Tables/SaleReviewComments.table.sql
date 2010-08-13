CREATE TABLE [dbo].[SaleReviewComments] (
    [CommentID]        BIGINT           IDENTITY (1, 1) NOT NULL,
    [ReviewID]         BIGINT           NOT NULL,
    [AuthorUserID]     UNIQUEIDENTIFIER NOT NULL,
    [MessageText]      NVARCHAR (MAX)   NOT NULL,
    [DateOfSubmission] DATETIME         NOT NULL
);

