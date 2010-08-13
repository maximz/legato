ALTER TABLE [dbo].[StoreReviewFlags]
    ADD CONSTRAINT [FK_StoreReviewFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

