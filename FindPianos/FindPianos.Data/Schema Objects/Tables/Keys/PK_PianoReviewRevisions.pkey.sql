﻿ALTER TABLE [dbo].[ReviewRevisions]
    ADD CONSTRAINT [PK_PianoReviewRevisions] PRIMARY KEY CLUSTERED ([ReviewRevisionID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
