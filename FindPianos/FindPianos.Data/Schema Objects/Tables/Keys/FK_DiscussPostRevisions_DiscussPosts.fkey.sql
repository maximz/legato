ALTER TABLE [dbo].[DiscussPostRevisions]
    ADD CONSTRAINT [FK_DiscussPostRevisions_DiscussPosts] FOREIGN KEY ([PostID]) REFERENCES [dbo].[DiscussPosts] ([PostID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

