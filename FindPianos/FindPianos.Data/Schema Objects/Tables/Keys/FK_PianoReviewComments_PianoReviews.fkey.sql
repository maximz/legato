ALTER TABLE [dbo].[ReviewComments]
    ADD CONSTRAINT [FK_PianoReviewComments_PianoReviews] FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[Reviews] ([ReviewID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

