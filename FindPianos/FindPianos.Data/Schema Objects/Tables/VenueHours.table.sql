CREATE TABLE [dbo].[VenueHours] (
    [VenueHoursID]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [ReviewRevisionID] BIGINT   NOT NULL,
    [DayOfWeek]        INT      NOT NULL,
    [StartTime]        DATETIME NULL,
    [EndTime]          DATETIME NULL
);

