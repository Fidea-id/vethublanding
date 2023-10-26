using VethubLanding.Interfaces;
using VethubLanding.Models;
using VethubLanding.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddControllersWithViews();
services.AddSingleton<HttpClient>();

services.AddScoped<IRestAPIService, RestAPIService>();
services.AddSingleton<IUriService>(provider =>
{
    var uri = new BaseURI();
    var keyEnvironment = builder.Configuration["MyAppSettings:Environment"];
    if (keyEnvironment == "LOCAL")
    {
        //LOCAL
        uri.APIURI = "https://localhost:44380/api/";
    }
    else if (keyEnvironment == "PRODUCTION")
    {
        //PRODUCTION
        uri.APIURI = "https://api.vethub.id/api/";
    }
    else if (keyEnvironment == "STAGING")
    {
        //STAGING
        uri.APIURI = "https://apisg.vethub.id/api/";
    }
    else
    {
        throw new Exception("Environment invalid");
    }
    return new UriService(uri);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
