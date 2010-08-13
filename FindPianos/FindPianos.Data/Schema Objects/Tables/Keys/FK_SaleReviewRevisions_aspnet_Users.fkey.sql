ALTER TABLE [dbo].[SaleReviewRevisions]
    ADD CONSTRAINT [FK_SaleReviewRevisions_aspnet_Users] FOREIGN KEY ([SubmitterUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

