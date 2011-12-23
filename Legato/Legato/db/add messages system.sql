USE [legato]
GO

/****** Object:  Table [dbo].[MessageFlags]    Script Date: 12/21/2011 18:21:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[Conversations](
	[ConversationID] [int] NOT NULL,
	[User1] [uniqueidentifier] NOT NULL,
	[User2] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[LastMessageDate] [datetime] NULL,
	[Subject] [varchar](100) NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_Conversations] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_aspnet_Users_1] FOREIGN KEY([User1])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_aspnet_Users_1]
GO

ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_aspnet_Users_2] FOREIGN KEY([User2])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_aspnet_Users_2]
GO

ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO

ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_GlobalPostIDs]
GO

CREATE TABLE [dbo].[Messages](
	[MessageID] [int] NOT NULL,
	[ConversationID] [int] NOT NULL,
	[SenderID] [uniqueidentifier] NOT NULL,
	[ReceipientID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Markdown] [nvarchar](max) NOT NULL,
	[Html] [nvarchar](max) NOT NULL,
	[NumberInConvo] [int] NOT NULL,
	[IsUnread] [bit] NOT NULL,
	[GlobalPostID] [int] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_aspnet_Users_1] FOREIGN KEY([SenderID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_aspnet_Users_1]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_aspnet_Users_2] FOREIGN KEY([ReceipientID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_aspnet_Users_2]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Conversations] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[Conversations] ([ConversationID])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Conversations]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_GlobalPostIDs]
GO


CREATE TABLE [dbo].[MessageFlags](
	[FlagID] [int] NOT NULL,
	[MessageID] [int] NOT NULL,
	[FlaggerID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[ModResponse] [bit] NULL,
 CONSTRAINT [PK_MessageFlags] PRIMARY KEY CLUSTERED 
(
	[FlagID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MessageFlags]  WITH CHECK ADD  CONSTRAINT [FK_MessageFlags_MessageFlags1] FOREIGN KEY([FlaggerID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[MessageFlags] CHECK CONSTRAINT [FK_MessageFlags_MessageFlags1]
GO

ALTER TABLE [dbo].[MessageFlags]  WITH CHECK ADD  CONSTRAINT [FK_MessageFlags_Messages] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Messages] ([MessageID])
GO

ALTER TABLE [dbo].[MessageFlags] CHECK CONSTRAINT [FK_MessageFlags_Messages]
GO

CREATE TABLE [dbo].[Notifications](
	[NotificationID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[GlobalPostID] [int] NOT NULL,
	[IsUnread] [bit] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_aspnet_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_aspnet_Users]
GO

ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_GlobalPostIDs] FOREIGN KEY([GlobalPostID])
REFERENCES [dbo].[GlobalPostIDs] ([GlobalPostID])
GO

ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_GlobalPostIDs]
GO

/* Forgot to add identify specifications/auto-increment ... */

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
ALTER TABLE dbo.Conversations
	DROP CONSTRAINT FK_Conversations_aspnet_Users_1
GO
ALTER TABLE dbo.Conversations
	DROP CONSTRAINT FK_Conversations_aspnet_Users_2
GO
ALTER TABLE dbo.aspnet_Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Conversations
	DROP CONSTRAINT FK_Conversations_GlobalPostIDs
GO
ALTER TABLE dbo.GlobalPostIDs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Conversations
	(
	ConversationID int NOT NULL IDENTITY (1, 1),
	User1 uniqueidentifier NOT NULL,
	User2 uniqueidentifier NOT NULL,
	StartDate datetime NOT NULL,
	LastMessageDate datetime NULL,
	Subject varchar(100) NOT NULL,
	GlobalPostID int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Conversations SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Conversations ON
GO
IF EXISTS(SELECT * FROM dbo.Conversations)
	 EXEC('INSERT INTO dbo.Tmp_Conversations (ConversationID, User1, User2, StartDate, LastMessageDate, Subject, GlobalPostID)
		SELECT ConversationID, User1, User2, StartDate, LastMessageDate, Subject, GlobalPostID FROM dbo.Conversations WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Conversations OFF
GO
ALTER TABLE dbo.Messages
	DROP CONSTRAINT FK_Messages_Conversations
GO
DROP TABLE dbo.Conversations
GO
EXECUTE sp_rename N'dbo.Tmp_Conversations', N'Conversations', 'OBJECT' 
GO
ALTER TABLE dbo.Conversations ADD CONSTRAINT
	PK_Conversations PRIMARY KEY CLUSTERED 
	(
	ConversationID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Conversations ON dbo.Conversations
	(
	User1
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Conversations_1 ON dbo.Conversations
	(
	User2
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Conversations ADD CONSTRAINT
	FK_Conversations_GlobalPostIDs FOREIGN KEY
	(
	GlobalPostID
	) REFERENCES dbo.GlobalPostIDs
	(
	GlobalPostID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Conversations ADD CONSTRAINT
	FK_Conversations_aspnet_Users_1 FOREIGN KEY
	(
	User1
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Conversations ADD CONSTRAINT
	FK_Conversations_aspnet_Users_2 FOREIGN KEY
	(
	User2
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	FK_Messages_Conversations FOREIGN KEY
	(
	ConversationID
	) REFERENCES dbo.Conversations
	(
	ConversationID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Messages SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* Next table: Messages */

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
ALTER TABLE dbo.Messages
	DROP CONSTRAINT FK_Messages_Conversations
GO
ALTER TABLE dbo.Conversations SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Messages
	DROP CONSTRAINT FK_Messages_GlobalPostIDs
GO
ALTER TABLE dbo.GlobalPostIDs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Messages
	DROP CONSTRAINT FK_Messages_aspnet_Users_1
GO
ALTER TABLE dbo.Messages
	DROP CONSTRAINT FK_Messages_aspnet_Users_2
GO
ALTER TABLE dbo.aspnet_Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Messages
	(
	MessageID int NOT NULL IDENTITY (1, 1),
	ConversationID int NOT NULL,
	SenderID uniqueidentifier NOT NULL,
	ReceipientID uniqueidentifier NOT NULL,
	Date datetime NOT NULL,
	Markdown nvarchar(MAX) NOT NULL,
	Html nvarchar(MAX) NOT NULL,
	NumberInConvo int NOT NULL,
	IsUnread bit NOT NULL,
	GlobalPostID int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Messages SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Messages ON
GO
IF EXISTS(SELECT * FROM dbo.Messages)
	 EXEC('INSERT INTO dbo.Tmp_Messages (MessageID, ConversationID, SenderID, ReceipientID, Date, Markdown, Html, NumberInConvo, IsUnread, GlobalPostID)
		SELECT MessageID, ConversationID, SenderID, ReceipientID, Date, Markdown, Html, NumberInConvo, IsUnread, GlobalPostID FROM dbo.Messages WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Messages OFF
GO
ALTER TABLE dbo.MessageFlags
	DROP CONSTRAINT FK_MessageFlags_Messages
GO
DROP TABLE dbo.Messages
GO
EXECUTE sp_rename N'dbo.Tmp_Messages', N'Messages', 'OBJECT' 
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	PK_Messages PRIMARY KEY CLUSTERED 
	(
	MessageID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Messages ON dbo.Messages
	(
	ConversationID,
	NumberInConvo
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	FK_Messages_aspnet_Users_1 FOREIGN KEY
	(
	SenderID
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	FK_Messages_aspnet_Users_2 FOREIGN KEY
	(
	ReceipientID
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	FK_Messages_GlobalPostIDs FOREIGN KEY
	(
	GlobalPostID
	) REFERENCES dbo.GlobalPostIDs
	(
	GlobalPostID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Messages ADD CONSTRAINT
	FK_Messages_Conversations FOREIGN KEY
	(
	ConversationID
	) REFERENCES dbo.Conversations
	(
	ConversationID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MessageFlags ADD CONSTRAINT
	FK_MessageFlags_Messages FOREIGN KEY
	(
	MessageID
	) REFERENCES dbo.Messages
	(
	MessageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MessageFlags SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

/* Next table: MessageFlags */

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
ALTER TABLE dbo.MessageFlags
	DROP CONSTRAINT FK_MessageFlags_Messages
GO
ALTER TABLE dbo.Messages SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MessageFlags
	DROP CONSTRAINT FK_MessageFlags_MessageFlags1
GO
ALTER TABLE dbo.aspnet_Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_MessageFlags
	(
	FlagID int NOT NULL IDENTITY (1, 1),
	MessageID int NOT NULL,
	FlaggerID uniqueidentifier NOT NULL,
	Date datetime NOT NULL,
	ModResponse bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_MessageFlags SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_MessageFlags ON
GO
IF EXISTS(SELECT * FROM dbo.MessageFlags)
	 EXEC('INSERT INTO dbo.Tmp_MessageFlags (FlagID, MessageID, FlaggerID, Date, ModResponse)
		SELECT FlagID, MessageID, FlaggerID, Date, ModResponse FROM dbo.MessageFlags WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_MessageFlags OFF
GO
DROP TABLE dbo.MessageFlags
GO
EXECUTE sp_rename N'dbo.Tmp_MessageFlags', N'MessageFlags', 'OBJECT' 
GO
ALTER TABLE dbo.MessageFlags ADD CONSTRAINT
	PK_MessageFlags PRIMARY KEY CLUSTERED 
	(
	FlagID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.MessageFlags ADD CONSTRAINT
	FK_MessageFlags_MessageFlags1 FOREIGN KEY
	(
	FlaggerID
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.MessageFlags ADD CONSTRAINT
	FK_MessageFlags_Messages FOREIGN KEY
	(
	MessageID
	) REFERENCES dbo.Messages
	(
	MessageID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT

/* Next table: Notifications */

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
ALTER TABLE dbo.Notifications
	DROP CONSTRAINT FK_Notifications_GlobalPostIDs
GO
ALTER TABLE dbo.GlobalPostIDs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Notifications
	DROP CONSTRAINT FK_Notifications_aspnet_Users
GO
ALTER TABLE dbo.aspnet_Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Notifications
	(
	NotificationID int NOT NULL IDENTITY (1, 1),
	UserID uniqueidentifier NOT NULL,
	Date datetime NOT NULL,
	GlobalPostID int NOT NULL,
	IsUnread bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Notifications SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Notifications ON
GO
IF EXISTS(SELECT * FROM dbo.Notifications)
	 EXEC('INSERT INTO dbo.Tmp_Notifications (NotificationID, UserID, Date, GlobalPostID, IsUnread)
		SELECT NotificationID, UserID, Date, GlobalPostID, IsUnread FROM dbo.Notifications WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Notifications OFF
GO
DROP TABLE dbo.Notifications
GO
EXECUTE sp_rename N'dbo.Tmp_Notifications', N'Notifications', 'OBJECT' 
GO
ALTER TABLE dbo.Notifications ADD CONSTRAINT
	PK_Notifications PRIMARY KEY CLUSTERED 
	(
	NotificationID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Notifications ON dbo.Notifications
	(
	UserID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Notifications_1 ON dbo.Notifications
	(
	UserID,
	Date DESC
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Notifications ADD CONSTRAINT
	FK_Notifications_aspnet_Users FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Notifications ADD CONSTRAINT
	FK_Notifications_GlobalPostIDs FOREIGN KEY
	(
	GlobalPostID
	) REFERENCES dbo.GlobalPostIDs
	(
	GlobalPostID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
