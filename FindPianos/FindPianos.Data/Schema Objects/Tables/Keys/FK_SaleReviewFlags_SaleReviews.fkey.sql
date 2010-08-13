ALTER TABLE [dbo].[SaleReviewFlags]
    ADD CONSTRAINT [FK_SaleReviewFlags_SaleReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[SaleReviews] ([SaleReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

