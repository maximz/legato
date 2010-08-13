ALTER TABLE [dbo].[SaleReviewRevisions]
    ADD CONSTRAINT [FK_SaleReviewRevisions_SaleReviews] FOREIGN KEY ([SaleReviewID]) REFERENCES [dbo].[SaleReviews] ([SaleReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

