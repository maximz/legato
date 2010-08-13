ALTER TABLE [dbo].[StoreListingComments]
    ADD CONSTRAINT [FK_StoreListingComments_StoreListings] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[StoreListings] ([StoreListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

