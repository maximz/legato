ALTER TABLE [dbo].[ReviewFlags]
    ADD CONSTRAINT [FK_PianoReviewFlags_PianoReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[Reviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

