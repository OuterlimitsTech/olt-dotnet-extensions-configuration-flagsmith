using System.Collections.Generic;
using System.Text.RegularExpressions;
using Flagsmith;
using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.Flagsmith
{
    public class FlagsmithConfigurationProvider : ConfigurationProvider
    {
        private readonly FlagsmithConfigurationSource _source;
        private static readonly object LockObject = new object();
        public FlagsmithConfigurationProvider(FlagsmithConfigurationSource source)
        {
            _source = source;
        }

        public override void Load()
        {

            var data = new Dictionary<string, string>();

            if (FlagsmithClient.instance == null)
                lock (LockObject)
                    if (FlagsmithClient.instance == null)
                    {
                        var configuration = new FlagsmithConfiguration
                        {
                            ApiUrl = _source.Options.ApiUrl,
                            EnvironmentKey = _source.Options.EnvironmentKey
                        };
                        var flagsmithClient = new FlagsmithClient(configuration);
                    }


            var flags = FlagsmithClient.instance.GetFeatureFlags().GetAwaiter().GetResult();

            if (flags != null)
            {
                var regex = new Regex(@"\b[A-Z]", RegexOptions.IgnoreCase);
                foreach (Flag flag in flags)
                {
                    if (flag.IsEnabled() || !_source.Options.EnabledOnly)
                    {
                        var rawName = flag.GetFeature().GetName();
                        var name = rawName;
                        if (rawName.Contains("__"))
                        {
                            name = regex.Replace(rawName.Replace("__", " "), m => m.ToString().ToUpper()).Replace(" ", string.Empty);
                        }
                        data.Add(name, flag.GetValue());
                    }

                }
            }


            Data = data;

        }



    }
}