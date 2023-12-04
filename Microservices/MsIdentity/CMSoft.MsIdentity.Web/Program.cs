using CMSoft.MsIdentity.Infrastructure.Repositories.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MsIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MsIdentityDbContextConnection' not found.");

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

// Add services to the container.

//  Add DB Configurations
services.AddDbContext<MsIdentityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

services.AddDatabaseDeveloperPageExceptionFilter();

services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MsIdentityDbContext>();


// Get dynamic credential
// AuthenticationConfig authConfig  =  Some method

services.AddAuthentication(options =>
{
    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie()
.AddGoogle("CustomGmail1", googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google1:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google1:ClientSecret"];
})
.AddGoogle("CustomGmail2", googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google2:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google2:ClientSecret"];
})
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
}).AddOpenIdConnect(options =>
{
    options.ClientId = "YourClientId";
    options.Authority = "https://login.microsoftonline.com/YourTenantId";
    options.ResponseType = OpenIdConnectResponseType.IdToken;
    options.SaveTokens = true;
}).AddWsFederation(options =>
{
    options.MetadataAddress = "https://fs.argos.co/federationMetadata/2007-06/FederationMetadata.xml";
    options.Wtrealm = "https://federationservices-dev.jujutests.com";
    options.CallbackPath = "/signin-adfs";
});


//builder.Services.AddAuthentication()
//   .AddFacebook(options =>
//   {
//       IConfigurationSection FBAuthNSection =
//       config.GetSection("Authentication:FB");
//       options.ClientId = FBAuthNSection["ClientId"];
//       options.ClientSecret = FBAuthNSection["ClientSecret"];
//   })
//   .AddTwitter(twitterOptions =>
//   {
//       twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
//       twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
//       twitterOptions.RetrieveUserDetails = true;
//   });
services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Start migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MsIdentityDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
