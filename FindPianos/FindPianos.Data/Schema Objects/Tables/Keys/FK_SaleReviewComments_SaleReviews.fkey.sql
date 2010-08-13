ALTER TABLE [dbo].[SaleReviewComments]
    ADD CONSTRAINT [FK_SaleReviewComments_SaleReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[SaleReviews] ([SaleReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

