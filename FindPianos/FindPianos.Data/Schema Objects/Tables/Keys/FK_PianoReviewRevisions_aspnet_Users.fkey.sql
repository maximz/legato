ALTER TABLE [dbo].[ReviewRevisions]
    ADD CONSTRAINT [FK_PianoReviewRevisions_aspnet_Users] FOREIGN KEY ([SubmitterUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

