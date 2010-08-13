ALTER TABLE [dbo].[StoreReviewComments]
    ADD CONSTRAINT [FK_StoreReviewComments_aspnet_Users] FOREIGN KEY ([AuthorUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

