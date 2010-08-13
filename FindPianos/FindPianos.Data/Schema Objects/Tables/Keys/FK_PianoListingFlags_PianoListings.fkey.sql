ALTER TABLE [dbo].[ListingFlags]
    ADD CONSTRAINT [FK_PianoListingFlags_PianoListings] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[Listings] ([ListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

