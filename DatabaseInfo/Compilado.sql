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
    db.name, mf.type_desc, mf.physical_name 
FROM 
    sys.databases db INNER JOIN 
	sys.master_files mf 
	    ON(db.database_id = mf.database_id)
	INNER JOIN sys.database_files dbf 
	    ON(mf.file_id = dbf.file_id);


SELECT * FROM sys.dm_db_file_space_usage;

SELECT * FROM sys.login_token;

SELECT * FROM sys.sql_logins;

SELECT * FROM sys.database_files;

SELECT * FROM sys.databases;

SELECT * FROM sys.master_files;