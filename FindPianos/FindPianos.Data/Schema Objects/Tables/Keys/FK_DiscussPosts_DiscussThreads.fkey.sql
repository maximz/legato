ALTER TABLE [dbo].[DiscussPosts]
    ADD CONSTRAINT [FK_DiscussPosts_DiscussThreads] FOREIGN KEY ([ThreadID]) REFERENCES [dbo].[DiscussThreads] ([ThreadID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

