ALTER TABLE [dbo].[SaleReviewFlags]
    ADD CONSTRAINT [FK_SaleReviewFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

