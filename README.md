# BankingWebAPI

ASP.NET Core Web API for the banking assignment.

## Local configuration

Configure local secrets with .NET user secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your SQL Server connection string>" --project .\BankingWebAPI\BankingWebAPI.csproj
dotnet user-secrets set "Jwt:Key" "<use a long random secret>" --project .\BankingWebAPI\BankingWebAPI.csproj
dotnet user-secrets set "Jwt:Issuer" "BankingWebAPI" --project .\BankingWebAPI\BankingWebAPI.csproj
dotnet user-secrets set "Jwt:Audience" "BankingWebAPI" --project .\BankingWebAPI\BankingWebAPI.csproj
dotnet user-secrets set "SeedAdmin:Username" "admin" --project .\BankingWebAPI\BankingWebAPI.csproj
dotnet user-secrets set "SeedAdmin:Password" "<use a strong local password>" --project .\BankingWebAPI\BankingWebAPI.csproj
```

The secrets are stored outside the repository and must not be committed.

Run the API with:

```powershell
dotnet run --project .\BankingWebAPI\BankingWebAPI.csproj
```

