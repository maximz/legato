ALTER TABLE [dbo].[Resumes]
    ADD CONSTRAINT [FK_Resumes_ResumeTypes] FOREIGN KEY ([ResumeTypeID]) REFERENCES [dbo].[ResumeTypes] ([ResumeTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

