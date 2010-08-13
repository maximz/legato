ALTER TABLE [dbo].[SaleListings]
    ADD CONSTRAINT [FK_SaleListings_aspnet_Users] FOREIGN KEY ([OriginalSubmitterUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

