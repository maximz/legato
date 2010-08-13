ALTER TABLE [dbo].[DiscussPostRevisions]
    ADD CONSTRAINT [FK_DiscussPostRevisions_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

