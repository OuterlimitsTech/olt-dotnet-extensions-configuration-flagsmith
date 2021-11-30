namespace OLT.Extensions.Configuration.Flagsmith
{
    public class FlagsmithConfigurationOptions
    {
        public FlagsmithConfigurationOptions()
        {

        }

        public FlagsmithConfigurationOptions(string environmentKey)
        {
            EnvironmentKey = environmentKey;
        }

        /// <summary>
        /// Api Url 
        /// </summary>
        /// <remarks>
        /// Default: https://api.flagsmith.com/api/v1/
        /// </remarks>
        public string ApiUrl { get; set; } = "https://api.flagsmith.com/api/v1/";

        /// <summary>
        /// API Key for the environment found under [EnvironmentName] -> Settings
        /// </summary>
        public string EnvironmentKey { get; set; }

        /// <summary>
        /// Only load the Flags that are enabled
        /// </summary>
        /// <remarks>
        /// The default value is true
        /// </remarks>
        public bool EnabledOnly { get; set; } = true;
    }
}
