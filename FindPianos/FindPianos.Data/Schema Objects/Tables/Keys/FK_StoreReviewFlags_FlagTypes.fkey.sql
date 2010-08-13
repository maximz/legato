ALTER TABLE [dbo].[StoreReviewFlags]
    ADD CONSTRAINT [FK_StoreReviewFlags_FlagTypes] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[FlagTypes] ([FlagTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

