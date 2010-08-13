ALTER TABLE [dbo].[SaleReviewFlags]
    ADD CONSTRAINT [FK_SaleReviewFlags_FlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

