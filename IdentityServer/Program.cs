using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    .AddTestUsers(IdentityServerConfig.TestUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
