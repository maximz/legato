ALTER TABLE [dbo].[DiscussPostRevisions]
    ADD CONSTRAINT [FK_DiscussPostRevisions_DiscussPosts1] FOREIGN KEY ([InReplyToPostID]) REFERENCES [dbo].[DiscussPosts] ([PostID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

