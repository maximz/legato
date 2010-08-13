ALTER TABLE [dbo].[Listings]
    ADD CONSTRAINT [FK_Listings_Instruments] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instruments] ([InstrumentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

