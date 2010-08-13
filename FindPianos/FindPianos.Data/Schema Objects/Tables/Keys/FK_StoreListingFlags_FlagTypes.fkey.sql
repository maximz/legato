ALTER TABLE [dbo].[StoreListingFlags]
    ADD CONSTRAINT [FK_StoreListingFlags_FlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

