using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OLT.Extensions.Configuration.Flagsmith.Tests
{
    public class Startup
    {

        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureHostConfiguration(builder =>
            {
                builder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();                    
            });
        }


        public virtual void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            services.Configure<AppSettingsDto>(hostBuilderContext.Configuration.GetSection("AppSettings"));
        }

    }
}
