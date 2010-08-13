ALTER TABLE [dbo].[ListingFlags]
    ADD CONSTRAINT [FK_PianoListingFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

