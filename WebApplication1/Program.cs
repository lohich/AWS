using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Program
    {
        private static string environment;

        public static void Main(string[] args)
        {
#if DEBUG
            environment = "Development";
#elif RELEASE
            environment = "Release";
#endif
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseEnvironment(environment)
                .ConfigureLogging(logBuilder =>
                                  {
                                      logBuilder.ClearProviders();
                                      logBuilder.AddConsole();
                                  })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
