# RunGroopWebApp

## How to run

- Change the database configuration in `appsettings.json`
- Change the CloudinarySettings configuration in `appsettings.json`
- Add your IPinfo access token in `HomeController.cs`
- On Developer Powershell:
- `dotnet tool install --global dotnet-ef`
- `dotnet ef database update`
- And for the Seed Data: `dotnet run seeddata`
- Finally run the application with `dotnet run`
