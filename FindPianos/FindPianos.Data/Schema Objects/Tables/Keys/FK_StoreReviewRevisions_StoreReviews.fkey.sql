ALTER TABLE [dbo].[StoreReviewRevisions]
    ADD CONSTRAINT [FK_StoreReviewRevisions_StoreReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[StoreReviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

