﻿ALTER TABLE [dbo].[UserSuspensions]
    ADD CONSTRAINT [FK_PianoUserSuspensions_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

