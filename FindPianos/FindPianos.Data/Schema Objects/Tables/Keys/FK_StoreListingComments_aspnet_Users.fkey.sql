ALTER TABLE [dbo].[StoreListingComments]
    ADD CONSTRAINT [FK_StoreListingComments_aspnet_Users] FOREIGN KEY ([AuthorUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

