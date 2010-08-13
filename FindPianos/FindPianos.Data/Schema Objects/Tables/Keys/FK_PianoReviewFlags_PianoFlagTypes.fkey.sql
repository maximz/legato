ALTER TABLE [dbo].[ReviewFlags]
    ADD CONSTRAINT [FK_PianoReviewFlags_PianoFlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

