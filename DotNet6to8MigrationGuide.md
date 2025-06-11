# .NET 6 to .NET 8 Migration Guide

## Project Overview
This migration guide outlines the steps to upgrade a .NET 6 Web API project to .NET 8. The project is a simple user management API with CRUD operations, using Entity Framework Core with an in-memory database.

## Migration Steps

### 1. Update Project File
- Update the target framework in `WebApi.csproj`:
  ```xml
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable> <!-- Consider adding this for improved null safety -->
  </PropertyGroup>
  ```

### 2. Update NuGet Packages
- Update all NuGet packages to versions compatible with .NET 8:
  ```xml
  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="12.0.1" />
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
  </ItemGroup>
  ```

### 3. Program.cs Updates
- The Program.cs structure is already using the minimal API approach introduced in .NET 6
- Consider these .NET 8 specific enhancements:
  - Add JSON source generator for improved performance:
    ```csharp
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }).AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    });
    ```
  - Use the new WebApplication.CreateSlimBuilder() for improved startup performance if minimal features are needed:
    ```csharp
    var builder = WebApplication.CreateSlimBuilder(args);
    ```
  - Consider using typed results for controllers:
    ```csharp
    app.MapControllers();
    ```

### 4. Entity Framework Core Updates
- Update DataContext.cs to use the new features in EF Core 8:
  - Consider using the new raw SQL query capabilities
  - Implement query caching for frequently accessed data
  - Use the new JSON column support if applicable
  - Update to the new SaveChangesAsync pattern if needed

### 5. Authentication and Authorization
- If the application uses authentication, consider upgrading to the new identity features in .NET 8
- Update any JWT authentication to use the new token validation parameters

### 6. Performance Improvements
- Implement native AOT compilation for improved startup time and reduced memory usage
- Add the following to the project file:
  ```xml
  <PropertyGroup>
    <PublishAot>true</PublishAot>
  </PropertyGroup>
  ```
- Review code for compatibility with AOT compilation

### 7. New .NET 8 Features to Consider
- Rate limiting middleware for API protection
- Output caching for improved performance
- Keyed services for dependency injection
- Minimal API improvements if applicable
- HTTP/3 support for improved performance

### 8. Testing
- Update any test projects to .NET 8
- Ensure all tests pass after migration
- Test performance improvements

### 9. Deployment Considerations
- Update CI/CD pipelines to use .NET 8 SDK
- Update Docker containers to use .NET 8 base images
- Consider container size optimization with the new .NET 8 trimming features

### 10. Breaking Changes to Watch For
- Nullable reference types behavior changes
- JSON serialization changes
- Entity Framework Core query translation differences
- Authentication and authorization policy changes

## Code Examples

### Updated Program.cs
```csharp
using System.Text.Json.Serialization;
using WebApi.Helpers;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    // Add output caching for improved performance
    services.AddOutputCache();

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    
    // Add output caching middleware
    app.UseOutputCache();

    app.MapControllers();
}

app.Run("http://localhost:4000");
```

### Updated Controller with TypedResults
```csharp
[HttpGet]
public IActionResult GetAll()
{
    var users = _userService.GetAll();
    return TypedResults.Ok(users);
}

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return TypedResults.NotFound();
    return TypedResults.Ok(user);
}
```

## Resources
- [What's new in .NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Breaking changes in .NET 8](https://learn.microsoft.com/en-us/dotnet/core/compatibility/8.0)
- [ASP.NET Core in .NET 8](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0)
- [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew)
