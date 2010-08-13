ALTER TABLE [dbo].[Reviews]
    ADD CONSTRAINT [FK_PianoReviews_PianoListings] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[Listings] ([ListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

