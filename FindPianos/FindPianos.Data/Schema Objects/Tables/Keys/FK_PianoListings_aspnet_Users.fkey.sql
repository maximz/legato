ALTER TABLE [dbo].[Listings]
    ADD CONSTRAINT [FK_PianoListings_aspnet_Users] FOREIGN KEY ([OriginalSubmitterUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

