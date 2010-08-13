ALTER TABLE [dbo].[DiscussPostFlags]
    ADD CONSTRAINT [FK_DiscussPostFlags_DiscussPosts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[DiscussPosts] ([PostID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

