﻿ALTER TABLE [dbo].[SaleListings]
    ADD CONSTRAINT [PK_SaleListingID] PRIMARY KEY CLUSTERED ([SaleListingID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
