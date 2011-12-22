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
