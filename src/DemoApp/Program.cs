using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DemoApp;
using DemoApp.Services;
using Flowbite.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Required for prerendering (BlazorWasmPreRendering.Build)
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

// Required for prerendering (BlazorWasmPreRendering.Build)
// extract the service-registration process to the static local function.
static void ConfigureServices(IServiceCollection services, string baseAddress)
{
  services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
  services.AddFlowbite();
  services.AddScoped<PokemonService>();
  services.AddScoped<IAiChatService, AiChatService>();
}
