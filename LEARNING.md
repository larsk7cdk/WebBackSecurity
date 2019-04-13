### EF Core
1. Install Nuget packages 
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.AspNetCore.Identity.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.AspNetCore.Identity

2. Tilret startup.cs
3. Add-Migration InitialCreate -c IdentityDbContext
4. Update-Database -c IdentityDbContext