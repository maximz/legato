ALTER TABLE [dbo].[Listings]
    ADD CONSTRAINT [FK_Listings_InstrumentTypes] FOREIGN KEY ([InstrumentTypeID]) REFERENCES [dbo].[InstrumentTypes] ([TypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

