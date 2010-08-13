ALTER TABLE [dbo].[ReviewFlags]
    ADD CONSTRAINT [FK_PianoReviewFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

