ALTER TABLE [dbo].[SaleReviewComments]
    ADD CONSTRAINT [FK_SaleReviewComments_aspnet_Users] FOREIGN KEY ([AuthorUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

