ALTER TABLE [dbo].[SaleListingFlags]
    ADD CONSTRAINT [FK_SaleListingFlags_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

