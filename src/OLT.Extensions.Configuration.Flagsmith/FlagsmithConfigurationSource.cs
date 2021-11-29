using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.Flagsmith
{
    public class FlagsmithConfigurationSource : IConfigurationSource
    {

        public FlagsmithConfigurationSource(FlagsmithConfigurationOptions options)
        {
            Options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new FlagsmithConfigurationProvider(this);
        }

        public FlagsmithConfigurationOptions Options { get; }
    }
}