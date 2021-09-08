
USE [master]
GO
CREATE LOGIN [u_mika] WITH PASSWORD=N'XZ9GFNj4', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [MikaDB]
GO
CREATE USER [u_mika] FOR LOGIN [u_mika]
GO


USE MikaDB
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Table1')
BEGIN
	CREATE TABLE session_wait_stats (
		session_id	smallint
		,wait_type	nvarchar(60)
		,waiting_tasks_count	bigint
		,wait_time_ms	bigint
		,max_wait_time_ms	bigint
		,signal_wait_time_ms	bigint
	);
END
GO

CREATE OR ALTER PROCEDURE p_getData
AS
BEGIN

	select *
	from sys.dm_exec_session_wait_stats

END
GO

GRANT EXECUTE ON p_getData TO u_mika;



-- EXEC p_getData
