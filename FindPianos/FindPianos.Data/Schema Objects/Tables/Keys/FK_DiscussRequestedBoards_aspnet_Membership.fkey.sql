ALTER TABLE [dbo].[DiscussRequestedBoards]
    ADD CONSTRAINT [FK_DiscussRequestedBoards_aspnet_Membership] FOREIGN KEY ([RequestUserID]) REFERENCES [dbo].[aspnet_Membership] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

