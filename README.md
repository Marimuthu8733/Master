# Tour Management .NET 8 Web API

This project is the migration target for the legacy .NET Framework 4.7.2 Web Forms Tour Management application.

## Project Goals
- Modern .NET 8 Web API architecture
- Controllers and services using interface-implementation pattern
- Migrated models and business logic
- Updated to SDK-style project files
- All obsolete APIs refactored or replaced

## Getting Started
1. Ensure you have [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
2. Restore dependencies:
   ```powershell
   dotnet restore
   ```
3. Build the project:
   ```powershell
   dotnet build
   ```
4. Run the project:
   ```powershell
   dotnet run
   ```

## Migration Notes
- This project is being updated to match the original .NET Framework 4.7.2 logic and schema.
- All legacy code is being refactored to modern .NET 8 Core patterns.

## Next Steps
- Migrate models, controllers, services, and seed data from the legacy project.
- Ensure all business logic and data schema are consistent with the original application.
