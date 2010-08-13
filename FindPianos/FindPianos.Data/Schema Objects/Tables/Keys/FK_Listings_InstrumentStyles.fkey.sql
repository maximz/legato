ALTER TABLE [dbo].[Listings]
    ADD CONSTRAINT [FK_Listings_InstrumentStyles] FOREIGN KEY ([InstrumentStyleID]) REFERENCES [dbo].[InstrumentStyles] ([StyleID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

