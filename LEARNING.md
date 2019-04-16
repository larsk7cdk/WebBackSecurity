### EF Core
1. Install Nuget packages 
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.AspNetCore.Identity.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.AspNetCore.Identity

2. Tilret startup.cs

3a. Add-Migration InitialCreate -c IdentityDbContext -o Migrations/Identity
3b. Add-Migration InitialCreate -c TodoDbContext -o Migrations/ToDo

4a. Update-Database -c IdentityDbContext
4b. Update-Database -c TodoDbContext