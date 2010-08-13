ALTER TABLE [dbo].[SaleListingFlags]
    ADD CONSTRAINT [FK_SaleListingFlags_FlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

