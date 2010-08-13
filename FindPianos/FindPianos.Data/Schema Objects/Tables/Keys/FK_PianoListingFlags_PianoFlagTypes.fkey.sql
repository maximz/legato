ALTER TABLE [dbo].[ListingFlags]
    ADD CONSTRAINT [FK_PianoListingFlags_PianoFlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

