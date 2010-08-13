CREATE TABLE [dbo].[StoreVenueHours] (
    [StoreVenueHoursID]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [StoreReviewRevisionID] BIGINT   NOT NULL,
    [DayOfWeek]             INT      NOT NULL,
    [StartTime]             DATETIME NULL,
    [EndTime]               DATETIME NULL
);

