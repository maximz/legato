ALTER TABLE [dbo].[SaleListings]
    ADD CONSTRAINT [FK_SaleListings_Instruments] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instruments] ([InstrumentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

