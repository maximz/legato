ALTER TABLE [dbo].[DiscussPostFlags]
    ADD CONSTRAINT [FK_DiscussPostFlags_FlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

