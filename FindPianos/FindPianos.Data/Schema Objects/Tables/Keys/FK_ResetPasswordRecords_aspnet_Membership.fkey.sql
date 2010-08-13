ALTER TABLE [dbo].[ResetPasswordRecords]
    ADD CONSTRAINT [FK_ResetPasswordRecords_aspnet_Membership] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Membership] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

