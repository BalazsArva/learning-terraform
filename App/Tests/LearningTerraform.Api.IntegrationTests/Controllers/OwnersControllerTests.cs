using System.Net.Http;
using LearningTerraform.ApiClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace LearningTerraform.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class OwnersControllerTests : ControllerTestBase
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GetById_OwnerDoesNotExist_ReturnsNotFound()
        {
            var client = CreateOwnersClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.GetByIdAsync(DefaultNonexistentEntityId));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        private OwnersClient CreateOwnersClient()
        {
            return new OwnersClient(Factory.ClientOptions.BaseAddress.ToString(), CreateHttpClient());
        }
    }

    public abstract class ControllerTestBase
    {
        public const string DefaultNonexistentEntityId = "DefaultNonexistentEntityId";

        public ControllerTestBase()
        {
            Factory = new DefaultWebApplicationFactory();
        }

        public WebApplicationFactory<Startup> Factory { get; }

        protected virtual HttpClient CreateHttpClient()
        {
            return Factory.CreateClient();
        }
    }

    public class DefaultWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });
        }
    }
}
