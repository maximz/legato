ALTER TABLE [dbo].[VenueHours]
    ADD CONSTRAINT [FK_PianoVenueHours_PianoVenues] FOREIGN KEY ([ReviewRevisionID]) REFERENCES [dbo].[ReviewRevisions] ([ReviewRevisionID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

