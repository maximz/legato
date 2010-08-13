EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'pianotoiletapp';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'pianotoiletapp';

