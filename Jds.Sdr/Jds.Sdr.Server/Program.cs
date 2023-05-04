using Jds.Sdr.Server;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
	.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();
builder.Services.AddDevExpressBlazor(options =>
{
	options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
	options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

builder.Services.AddApplicationDbContext();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddIdentity(builder);

builder.Services.AddLogging();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

const string defaultCulture = "es";
var supportedCultures = new[]
{
	new CultureInfo("es")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(defaultCulture);
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

string contentPath = app.Environment.ContentRootPath;
AppDomain.CurrentDomain.SetData("DXResourceDirectory", contentPath);

app.Run();