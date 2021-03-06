/*
   Saturday, June 23, 20129:26:48 PM
   User: 
   Server: MAXIM-LAPTOP
   Database: legato
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Instruments ADD
	DisplayedLat float(53) NULL,
	DisplayedLong float(53) NULL
GO
ALTER TABLE dbo.Instruments SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
