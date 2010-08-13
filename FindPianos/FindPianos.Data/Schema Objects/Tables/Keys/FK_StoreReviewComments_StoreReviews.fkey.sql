ALTER TABLE [dbo].[StoreReviewComments]
    ADD CONSTRAINT [FK_StoreReviewComments_StoreReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[StoreReviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

