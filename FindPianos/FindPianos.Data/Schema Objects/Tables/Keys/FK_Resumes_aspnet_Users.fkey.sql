ALTER TABLE [dbo].[Resumes]
    ADD CONSTRAINT [FK_Resumes_aspnet_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

