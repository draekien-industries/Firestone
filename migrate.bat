rmdir /S /Q src/Firestone.Infrastructure/Data/Migrations

dotnet ef migrations add FirestoneDb --project src/Firestone.Infrastructure --startup-project src/Firestone.Api -o Data/Migrations
