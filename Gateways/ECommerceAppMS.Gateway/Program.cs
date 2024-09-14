using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json", optional: false, reloadOnChange: false).AddEnvironmentVariables();

builder.Services.AddOcelot();
var app = builder.Build();

await app.UseOcelot();

app.Run();
