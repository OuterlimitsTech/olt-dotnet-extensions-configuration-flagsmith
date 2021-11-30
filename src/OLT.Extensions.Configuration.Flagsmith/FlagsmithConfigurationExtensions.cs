using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.Flagsmith
{
    public static class FlagsmithConfigurationExtensions
    {
        public static IConfigurationBuilder AddFlagsmith(this IConfigurationBuilder configuration, Action<FlagsmithConfigurationOptions> options)
        {
            var configOptions = new FlagsmithConfigurationOptions();
            options(configOptions);
            configuration.Add(new FlagsmithConfigurationSource(configOptions));
            return configuration;
        }
    }
}