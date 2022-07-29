using IdentityServer.Data;
using IdentityServerHost.Quickstart.UI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-4.0.0;trusted_connection=yes;";


builder.Services.AddControllersWithViews();

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(IdentityServerConfig.Clients)
//    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
//    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
//    .AddTestUsers(TestUsers.Users)
//    .AddDeveloperSigningCredential();

builder.Services.AddIdentityServer()
    .AddTestUsers(TestUsers.Users)
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
