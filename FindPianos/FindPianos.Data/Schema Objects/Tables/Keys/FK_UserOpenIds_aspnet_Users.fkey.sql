ALTER TABLE [dbo].[UserOpenIds]
    ADD CONSTRAINT [FK_UserOpenIds_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

