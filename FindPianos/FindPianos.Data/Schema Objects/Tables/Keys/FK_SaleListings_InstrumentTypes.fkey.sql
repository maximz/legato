ALTER TABLE [dbo].[SaleListings]
    ADD CONSTRAINT [FK_SaleListings_InstrumentTypes] FOREIGN KEY ([InstrumentTypeID]) REFERENCES [dbo].[InstrumentTypes] ([TypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

