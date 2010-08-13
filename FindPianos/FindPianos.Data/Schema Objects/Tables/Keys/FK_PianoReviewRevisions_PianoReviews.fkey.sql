ALTER TABLE [dbo].[ReviewRevisions]
    ADD CONSTRAINT [FK_PianoReviewRevisions_PianoReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[Reviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

