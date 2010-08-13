ALTER TABLE [dbo].[ListingComments]
    ADD CONSTRAINT [FK_PianoListingComments_PianoReviews] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[Listings] ([ListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

