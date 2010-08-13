ALTER TABLE [dbo].[Jobs]
    ADD CONSTRAINT [FK_Jobs_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

