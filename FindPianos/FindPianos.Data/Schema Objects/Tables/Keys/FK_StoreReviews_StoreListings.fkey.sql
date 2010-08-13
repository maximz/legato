ALTER TABLE [dbo].[StoreReviews]
    ADD CONSTRAINT [FK_StoreReviews_StoreListings] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[StoreListings] ([StoreListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

