ALTER TABLE [dbo].[DiscussThreads]
    ADD CONSTRAINT [FK_DiscussThreads_DiscussBoards] FOREIGN KEY ([BoardID]) REFERENCES [dbo].[DiscussBoards] ([BoardID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

