﻿ALTER TABLE [dbo].[StoreReviewFlags]
    ADD CONSTRAINT [PK_StoreReviewFlags] PRIMARY KEY CLUSTERED ([FlagID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
