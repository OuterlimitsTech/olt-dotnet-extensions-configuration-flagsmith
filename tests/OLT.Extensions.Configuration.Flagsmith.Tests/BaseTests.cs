namespace OLT.Extensions.Configuration.Flagsmith.Tests
{
    public abstract class BaseTests
    {
#pragma warning disable CS8603 // Possible null reference return.
        public string ApiKey => System.Environment.GetEnvironmentVariable("FLAGSMITH_API_KEY");
#pragma warning restore CS8603 // Possible null reference return.

    }
}