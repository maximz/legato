﻿ALTER TABLE [dbo].[DiscussPosts]
    ADD CONSTRAINT [PK_DiscussPosts] PRIMARY KEY CLUSTERED ([PostID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
