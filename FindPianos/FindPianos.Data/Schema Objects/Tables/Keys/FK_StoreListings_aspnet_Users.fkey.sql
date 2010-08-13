ALTER TABLE [dbo].[StoreListings]
    ADD CONSTRAINT [FK_StoreListings_aspnet_Users] FOREIGN KEY ([SubmitterUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

