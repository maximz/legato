ALTER TABLE [dbo].[SaleListingFlags]
    ADD CONSTRAINT [FK_SaleListingFlags_SaleListings] FOREIGN KEY ([ListingID]) REFERENCES [dbo].[SaleListings] ([SaleListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

