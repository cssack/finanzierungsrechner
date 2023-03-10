global using System.Collections;
global using web.app._abstractions;
global using web.app.generators;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("de-AT");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("de-AT");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
