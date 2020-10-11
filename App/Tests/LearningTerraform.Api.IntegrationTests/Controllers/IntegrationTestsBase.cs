using System.Net.Http;
using System.Threading.Tasks;
using LearningTerraform.DataAccess.MsSql.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.Api.IntegrationTests.Controllers
{
    public abstract class IntegrationTestsBase
    {
        public const string DefaultNonexistentEntityId = "DefaultNonexistentEntityId";

        public const string DefaultOwnerPublicId = "c6aa50d20a57439ea027df37f9b34a54";
        public const string DefaultOwnerFirstName = "OwnerFirstName";
        public const string DefaultOwnerLastName = "OwnerLastName";

        public const string DefaultPetName1 = "DefaultPetName1";
        public const string DefaultPetName2 = "DefaultPetName2";
        public const string DefaultPetPublicId1 = "6cee7d445bc941a78b58022c115107a8";
        public const string DefaultPetPublicId2 = "1473674530a14d04aa9e8c3925d6e07e";

        protected IntegrationTestsBase()
        {
            Factory = new DefaultWebApplicationFactory();
        }

        public WebApplicationFactory<Startup> Factory { get; }

        protected async Task CleanupDatabaseAsync()
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
