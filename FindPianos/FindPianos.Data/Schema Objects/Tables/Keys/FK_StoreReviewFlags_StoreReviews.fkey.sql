ALTER TABLE [dbo].[StoreReviewFlags]
    ADD CONSTRAINT [FK_StoreReviewFlags_StoreReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[StoreReviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

