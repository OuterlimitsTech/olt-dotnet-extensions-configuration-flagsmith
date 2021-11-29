namespace OLT.Extensions.Configuration.Flagsmith
{
    public class FlagsmithConfigurationOptions
    {
        public string ApiUrl { get; set; }
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
