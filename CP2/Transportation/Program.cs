global using Xunit;

using Transportation.Interfaces;
using Transportation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ChatGPT //

builder.Services.AddSingleton<IAirplanes, Airbus>();
builder.Services.AddSingleton<IAirplanes, Boeing>();

builder.Services.AddAuthorization();

// ChatGPT //

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
