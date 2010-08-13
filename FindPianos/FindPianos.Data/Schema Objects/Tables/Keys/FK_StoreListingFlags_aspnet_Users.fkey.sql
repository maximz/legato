ALTER TABLE [dbo].[StoreListingFlags]
    ADD CONSTRAINT [FK_StoreListingFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

