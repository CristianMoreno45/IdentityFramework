dotnet tool install --global dotnet-aspnet-codegenerator --version 6.0.0
agregar  Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet aspnet-codegenerator identity -dc MsIdentityDbContext --files "Account.Register;Account.Login"