using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace OLT.Extensions.Configuration.Flagsmith.Tests
{
    public class UnitTests : BaseTests 
    {
        [Fact]
        public void TestConfig()
        {
            var t = Environment.GetEnvironmentVariable("FLAGSMITH_API_KEY");
            var apiKey = Faker.Internet.UserName();
            var apiUrl = Faker.Internet.Url();
            var options = new FlagsmithConfigurationOptions(apiKey)
            { 
                ApiUrl = apiUrl,
                EnabledOnly = false,
            };
            
            Assert.Equal(apiKey, options.EnvironmentKey);
            Assert.Equal(apiUrl, options.ApiUrl);
            Assert.False(options.EnabledOnly);

            options.EnabledOnly = true;
            Assert.Equal(apiKey, options.EnvironmentKey);
            Assert.Equal(apiUrl, options.ApiUrl);
            Assert.True(options.EnabledOnly);
                        
            options = new FlagsmithConfigurationOptions(base.ApiKey)
            {
                ApiUrl = apiUrl,
                EnabledOnly = false,
            };

            var x = options;
        }

        [Fact]
        public void TestServer()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", false, true)
                        .AddEnvironmentVariables();

                    var config = builder.Build();

                    builder.AddFlagsmith(options =>
                    {
                        options.ApiUrl = config.GetValue<string>("Flagsmith:ApiUrl");
                        options.EnvironmentKey = base.ApiKey;
                    });
                })
                .UseStartup<TestHostStartup>();

            var host = webBuilder.Build();

            var options  = host.Services.GetService<IOptions<AppSettingsDto>>();
            if (options != null)
            {
                var result = options.Value;
            }            

            Assert.True(true);
        }
    }
}