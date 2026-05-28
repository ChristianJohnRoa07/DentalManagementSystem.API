# Dental Management System — Database CLI Guide

## 1. Connect to the SQL Server Container

To interact with the SQL Server database directly via the command line, first connect to the running container and launch the `sqlcmd` utility:

```bash
docker exec -it dental_sql_server /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "DentalApp#2026!" -C
Note: The password is wrapped in quotes to ensure special characters like # and ! are processed correctly by the terminal.

2. Querying the Databases
Once you are connected to the SQL Server interface, you can immediately switch databases and query them. Always make sure to execute the USE command so you know which database you are currently targeting.

View Tables in the Application Database
SQL
USE DentalManagementSystem_Application_db;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
GO

