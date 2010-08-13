ALTER TABLE [dbo].[ReviewComments]
    ADD CONSTRAINT [FK_PianoReviewComments_aspnet_Users] FOREIGN KEY ([AuthorUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

