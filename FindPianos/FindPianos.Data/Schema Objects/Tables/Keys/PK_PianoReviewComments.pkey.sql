﻿ALTER TABLE [dbo].[ReviewComments]
    ADD CONSTRAINT [PK_PianoReviewComments] PRIMARY KEY CLUSTERED ([CommentID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

