﻿ALTER TABLE [dbo].[ReviewFlags]
    ADD CONSTRAINT [PK_PianoReviewFlags] PRIMARY KEY CLUSTERED ([FlagID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
