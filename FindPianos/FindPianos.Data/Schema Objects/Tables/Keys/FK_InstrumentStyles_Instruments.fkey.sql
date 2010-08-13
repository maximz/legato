ALTER TABLE [dbo].[InstrumentStyles]
    ADD CONSTRAINT [FK_InstrumentStyles_Instruments] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instruments] ([InstrumentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

