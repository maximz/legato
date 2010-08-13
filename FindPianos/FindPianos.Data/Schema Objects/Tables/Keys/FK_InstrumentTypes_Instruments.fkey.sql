ALTER TABLE [dbo].[InstrumentTypes]
    ADD CONSTRAINT [FK_InstrumentTypes_Instruments] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instruments] ([InstrumentID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

