﻿ALTER TABLE [dbo].[StoreReviews]
    ADD CONSTRAINT [PK_StoreReviews] PRIMARY KEY CLUSTERED ([ReviewID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

