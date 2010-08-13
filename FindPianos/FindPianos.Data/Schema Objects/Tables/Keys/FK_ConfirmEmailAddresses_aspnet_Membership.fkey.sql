ALTER TABLE [dbo].[ConfirmEmailAddresses]
    ADD CONSTRAINT [FK_ConfirmEmailAddresses_aspnet_Membership] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Membership] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

