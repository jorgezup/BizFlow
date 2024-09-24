dotnet ef migrations add CreateCustomerPreferencesTable -p src/Infrastructure/Infrastructure.csproj -s src/WebAPI/WebAPI.csproj

dotnet ef migrations add InitialCreate



-- List Migrations
dotnet ef migrations list -p src/Infrastructure/Infrastructure.csproj -s src/WebAPI/WebAPI.csproj 

-- Revert Migrations, change to Last Migrations you wanna keep
dotnet ef database update 20240726234608_CreatePaymentsTable -p src/Infrastructure/Infrastructure.csproj -s src/WebAPI/WebAPI.csproj 

