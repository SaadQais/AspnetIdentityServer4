using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-4.0.0;trusted_connection=yes;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(IdentityServerConfig.Clients)
//    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
//    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
//    .AddTestUsers(TestUsers.Users)
//    .AddDeveloperSigningCredential();

builder.Services.AddIdentityServer()
    .AddTestUsers(TestUsers.Users)
    .AddAspNetIdentity<ApplicationUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

InitializeDatabase.Seed(app);

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
