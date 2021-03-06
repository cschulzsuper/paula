using Microsoft.Extensions.Configuration;

namespace Super.Paula.Client.Configuration
{
    public class BlazorApiConfigurationSource : IConfigurationSource
    {
        private readonly IConfigurationProvider _provider;

        public BlazorApiConfigurationSource(IConfigurationProvider provider) =>
            _provider = provider;

        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            _provider;


    }
}
