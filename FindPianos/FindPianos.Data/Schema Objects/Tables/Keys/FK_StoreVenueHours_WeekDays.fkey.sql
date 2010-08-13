ALTER TABLE [dbo].[StoreVenueHours]
    ADD CONSTRAINT [FK_StoreVenueHours_WeekDays] FOREIGN KEY ([DayOfWeek]) REFERENCES [dbo].[WeekDays] ([WeekDayID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

