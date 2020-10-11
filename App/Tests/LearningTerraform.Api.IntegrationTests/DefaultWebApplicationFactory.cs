using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace LearningTerraform.Api.IntegrationTests
{
    public class DefaultWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly IDictionary<string, string> additionalConfiguration;

        public DefaultWebApplicationFactory(
            IDictionary<string, string> additionalConfiguration = null)
        {
            this.additionalConfiguration = additionalConfiguration;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });

            builder.ConfigureAppConfiguration(configuration =>
            {
                if (additionalConfiguration != null)
                {
                    configuration.AddInMemoryCollection(additionalConfiguration);
                }
            });
        }
    }
}
