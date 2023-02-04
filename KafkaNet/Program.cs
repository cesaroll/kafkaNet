using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args).Build();

Console.WriteLine("Hello, World!");

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Host created!");

await host.RunAsync();
