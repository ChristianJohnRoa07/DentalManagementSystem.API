# DentalManagementSystem


# Connect to dental_sql_server
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P DentalApp#2026! -C

Once inside database container, you can immediatly SELECT on the databases. Just take note of where database you are currently at.

DentalManagementSystem_Application_db
    USE DentalManagementSystem_Application_db;
    SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
    GO

DentalManagementSystem_Identity_db
    USE DentalManagementSystem_Identity_db;
    SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
    GO


# Create migration script
Add-Migration InitialApplicationCreate -Project DentalManagementSystem.Identity -StartupProject DentalManagementSystem.API -Context DentalManagementSystemDbContext
Add-Migration InitialIdentityCreate -Project DentalManagementSystem.Identity -StartupProject DentalManagementSystem.API -Context ApplicationUserDbContext

