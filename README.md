# CarRentalSystemAD

ASP.NET Core MVC car rental system for a final school project.

## Features

- ASP.NET Core 8 MVC
- Entity Framework Core with SQL Server
- ASP.NET Identity with Administrator role
- Cars CRUD
- Categories CRUD
- Rentals
- Reviews
- Admin area
- API controller
- AJAX dashboard
- Searching, sorting and paging
- Custom error pages
- NUnit tests

## Admin login

Email: admin@carrental.com  
Password: Admin123!

## Database

The project is configured for Docker SQL Server:

```json
"Server=localhost,1433;Database=CarRentalSystemAD;User Id=sa;Password=SuperStrongPass!23;TrustServerCertificate=true;"
```

## Commands

```powershell
dotnet restore
dotnet ef migrations add InitialCreate --project CarRentalSystemAD.Data --startup-project CarRentalSystemAD.Web
dotnet ef database update --project CarRentalSystemAD.Data --startup-project CarRentalSystemAD.Web
dotnet run --project CarRentalSystemAD.Web
```
