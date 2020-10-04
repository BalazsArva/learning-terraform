using System.Net.Http;
using System.Threading.Tasks;
using LearningTerraform.DataAccess.MsSql.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.Api.IntegrationTests.Controllers
{
    public abstract class ControllerTestBase
    {
        public const string DefaultNonexistentEntityId = "DefaultNonexistentEntityId";

        public ControllerTestBase()
        {
            Factory = new DefaultWebApplicationFactory();
        }

        public WebApplicationFactory<Startup> Factory { get; }

        protected async Task CleanupDatabase()
        {
            using var cleanupContext = CreateDataContext();

            cleanupContext.Pets.RemoveRange(await cleanupContext.Pets.ToListAsync());
            cleanupContext.Owners.RemoveRange(await cleanupContext.Owners.ToListAsync());

            await cleanupContext.SaveChangesAsync();
        }

        protected DataContext CreateDataContext()
        {
            return Factory.Services.GetRequiredService<DataContext>();
        }

        protected HttpClient CreateHttpClient()
        {
            return Factory.CreateClient();
        }
    }
}
