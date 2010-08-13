ALTER TABLE [dbo].[SaleReviews]
    ADD CONSTRAINT [FK_SaleReviews_SaleListings] FOREIGN KEY ([SaleListingID]) REFERENCES [dbo].[SaleListings] ([SaleListingID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

