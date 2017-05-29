SELECT @@VERSION; 

SELECT @@CONNECTIONS;

SELECT @@SERVERNAME; --Nombre de la base

SELECT @@SERVICENAME; --Nombre de la instacia

/*SQL Server Memory values*/
SELECT 
    name, 
	value, 
	value_in_use, 
	description 
FROM 
    sys.configurations 
WHERE 
    name LIKE '%server memory%' 
ORDER BY name; 

/*Database general info*/
SELECT 
    name, 
	create_date, 
	state_desc, 
	recovery_model_desc 
FROM sys.databases; 


/*Specific database info*/
SELECT 
    SUSER_SNAME(db.owner_sid) AS Propietario,
    db.name, mfrows.RowSize, mflog.LogSize,
	CASE
		WHEN dbf.max_size = 0 THEN 'NoGrowthAllowed'
		WHEN dbf.max_size = -1 THEN 'UntilFullDisk'
	 END, 
	mf.type_desc, mf.physical_name 
FROM 
    sys.databases db INNER JOIN 
	sys.master_files mf 
	    ON(db.database_id = mf.database_id)
	INNER JOIN sys.database_files dbf 
	    ON(mf.file_id = dbf.file_id)
	LEFT JOIN (SELECT database_id, SUM(size) RowSize FROM sys.master_files WHERE type = 0 GROUP BY database_id, type) AS mfrows 
		ON (mfrows.database_id = db.database_id)
	LEFT JOIN (SELECT database_id, SUM(size) LogSize FROM sys.master_files WHERE type = 1 GROUP BY database_id, type) AS mflog 
		ON (mflog.database_id = db.database_id);

SELECT * FROM sys.dm_db_file_space_usage;

SELECT * FROM sys.login_token;

SELECT * FROM sys.sql_logins;

SELECT * FROM sys.database_files;

SELECT * FROM sys.databases;

SELECT * FROM sys.master_files;
/*Conexiones activas en SQL Server*/
SELECT * FROM sys.dm_exec_connections; 
/*Sessiones activas en SQL Server*/
SELECT * FROM sys.dm_exec_sessions;

/*Informacion del host*/
SELECT * FROM sys.dm_os_host_info

SELECT * FROM sys.dm_exec_query_optimizer_info;


SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key LIKE N'%SuperSocketNetLib%';