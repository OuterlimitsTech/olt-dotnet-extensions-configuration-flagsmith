using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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

        }

        [Theory]
        [InlineData(false, "Remote_JwtSecret", "Remote_ConfigValue1", "Remote_ConfigValue2")]
        [InlineData(true, "Remote_JwtSecret", "Local_ConfigValue1", "Remote_ConfigValue2")]
        public void TestServer(bool enabledOnly, string expectedJwt, string expectedConfig1, string expectedConfig2)
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
                        options.EnabledOnly = enabledOnly;
                    });
                })
                .UseStartup<TestHostStartup>();
            
            using (var server = new TestServer(webBuilder))
            {                
                var options = server.Host.Services.GetService<IOptions<AppSettingsDto>>();
                if (options != null)
                {
                    Assert.Equal(expectedJwt, options.Value.JwtSecret);
                    Assert.Equal(expectedConfig1, options.Value.ConfigValue1);
                    Assert.Equal(expectedConfig2, options.Value.ConfigValue2);
                }

            }
            
        }


       
    }
}