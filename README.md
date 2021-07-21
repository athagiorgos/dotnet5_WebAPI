# dotnet5_WebAPI

In order for the API to work you need to download dotnet 5 runtime environment (cross-platform) and run command dotnet run.
This API is a first look at dotnet5 and c# and comes with Swagger Ui to test it. 
UPDATE: In order to work with the database some packages from dotnet need to be installed.
Execute the below commands:
dotnet add package Microsoft.EntityFramewrokCore.SqlServer,
dotnet add package Microsoft.EntityFrameworkCore.Design,
dotnet tool install --global dotnet-ef.
To update and query the database, a schema is ready (migration has been created and exists in files).
Execute: dotnet ef database update.
***NOTE: In appsettings.json in ConnectionStrings the DeafaultConnection configuration is set to connect to SQLEXPRESS.
You can change the database and table name in there.***
Enjoy!!!
