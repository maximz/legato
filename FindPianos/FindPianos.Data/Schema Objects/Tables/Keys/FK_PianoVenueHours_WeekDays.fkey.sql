ALTER TABLE [dbo].[VenueHours]
    ADD CONSTRAINT [FK_PianoVenueHours_WeekDays] FOREIGN KEY ([DayOfWeek]) REFERENCES [dbo].[WeekDays] ([WeekDayID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

