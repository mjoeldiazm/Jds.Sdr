using Jds.Sdr.WebClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDevExpressBlazor(options =>
{
	options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
	options.SizeMode = DevExpress.Blazor.SizeMode.Small;
	options.ShowValidationIcon = true;
});
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddAuthorization();
builder.Services.AddDataRepositories();
builder.Services.AddEntityControllerProvider();

var host = builder.Build();
var culture = new CultureInfo("es");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
await host.RunAsync();