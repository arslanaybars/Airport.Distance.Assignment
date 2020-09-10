using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace Ada.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog(delegate (WebHostBuilderContext context, LoggerConfiguration configuration)
                    {
                        configuration.WriteTo.File(new RenderedCompactJsonFormatter(), "logs/logs.txt",
                            rollingInterval: RollingInterval.Day);
                    });
                });
    }
}
