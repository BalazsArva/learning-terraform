using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        public async Task GetById_OwnerDoesNotExist_ReturnsNotFound()
        {
            var client = Factory.CreateClient();
        }
    }

    public abstract class ControllerTestBase
    {
        public ControllerTestBase()
        {
            Factory = new DefaultWebApplicationFactory();
        }

        public WebApplicationFactory<Startup> Factory { get; }
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
