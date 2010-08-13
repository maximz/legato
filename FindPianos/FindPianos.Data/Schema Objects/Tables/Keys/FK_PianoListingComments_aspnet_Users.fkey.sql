ALTER TABLE [dbo].[ListingComments]
    ADD CONSTRAINT [FK_PianoListingComments_aspnet_Users] FOREIGN KEY ([AuthorUserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

