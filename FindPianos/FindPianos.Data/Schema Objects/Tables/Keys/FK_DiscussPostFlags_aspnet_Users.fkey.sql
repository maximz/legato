ALTER TABLE [dbo].[DiscussPostFlags]
    ADD CONSTRAINT [FK_DiscussPostFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

