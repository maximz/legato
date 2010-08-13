ALTER TABLE [dbo].[SaleListings]
    ADD CONSTRAINT [FK_SaleListings_InstrumentStyles] FOREIGN KEY ([InstrumentStyleID]) REFERENCES [dbo].[InstrumentStyles] ([StyleID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

