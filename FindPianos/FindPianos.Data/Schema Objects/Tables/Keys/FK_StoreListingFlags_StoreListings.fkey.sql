ALTER TABLE [dbo].[StoreListingFlags]
    ADD CONSTRAINT [FK_StoreListingFlags_StoreListings] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[StoreListings] ([StoreListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

