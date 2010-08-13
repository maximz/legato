ALTER TABLE [dbo].[StoreVenueHours]
    ADD CONSTRAINT [FK_StoreVenueHours_StoreReviewRevisions] FOREIGN KEY ([StoreReviewRevisionID]) REFERENCES [dbo].[StoreReviewRevisions] ([StoreReviewRevisionID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

