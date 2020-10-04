using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LearningTerraform.ApiClient;
using LearningTerraform.DataAccess.MsSql.Database;
using LearningTerraform.DataAccess.MsSql.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LearningTerraform.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class OwnersControllerTests : ControllerTestBase
    {
        private const string DefaultPublicId = "c6aa50d20a57439ea027df37f9b34a54";
        private const string DefaultFirstName = "OwnerFirstName";
        private const string DefaultLastName = "OwnerLastName";

        private const string DefaultPetName1 = "DefaultPetName1";
        private const string DefaultPetName2 = "DefaultPetName2";
        private const string DefaultPetPublicId1 = "6cee7d445bc941a78b58022c115107a8";
        private const string DefaultPetPublicId2 = "1473674530a14d04aa9e8c3925d6e07e";

        [SetUp]
        public async Task SetUpAsync()
        {
            await CleanupDatabase();
        }

        [Test]
        public void GetByIdAsync_OwnerDoesNotExist_ReturnsNotFound()
        {
            var client = CreateOwnersClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.GetByIdAsync(DefaultNonexistentEntityId));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        public async Task GetByIdAsync_OwnerHasNoPets_ReturnsOwnerWithZeroPets()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultFirstName,
                    LastName = DefaultLastName,
                    PublicId = DefaultPublicId,
                };

                preparationContext.Owners.Add(ownerDbEntity);

                await preparationContext.SaveChangesAsync();
            }

            var client = CreateOwnersClient();

            var owner = await client.GetByIdAsync(DefaultPublicId);

            Assert.IsNotNull(owner);

            Assert.AreEqual(DefaultPublicId, owner.Id);
            Assert.AreEqual(DefaultFirstName, owner.FirstName);
            Assert.AreEqual(DefaultLastName, owner.LastName);
        }

        [Test]
        public async Task GetByIdAsync_OwnerHasPets_ReturnsOwnerWithAllPets()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultFirstName,
                    LastName = DefaultLastName,
                    PublicId = DefaultPublicId,
                };

                var petEntity1 = new Pet
                {
                    Name = DefaultPetName1,
                    PublicId = DefaultPetPublicId1,
                };

                var petEntity2 = new Pet
                {
                    Name = DefaultPetName2,
                    PublicId = DefaultPetPublicId2,
                };

                ownerDbEntity.Pets.Add(petEntity1);
                ownerDbEntity.Pets.Add(petEntity2);

                preparationContext.Owners.Add(ownerDbEntity);
                preparationContext.Pets.Add(petEntity1);
                preparationContext.Pets.Add(petEntity2);

                await preparationContext.SaveChangesAsync();
            }

            var client = CreateOwnersClient();

            var owner = await client.GetByIdAsync(DefaultPublicId);

            Assert.IsNotNull(owner);

            Assert.AreEqual(DefaultPublicId, owner.Id);
            Assert.AreEqual(DefaultFirstName, owner.FirstName);
            Assert.AreEqual(DefaultLastName, owner.LastName);

            Assert.AreEqual(2, owner.Pets.Count);

            Assert.AreEqual(DefaultPetPublicId1, owner.Pets[0].Id);
            Assert.AreEqual(DefaultPetName1, owner.Pets[0].Name);

            Assert.AreEqual(DefaultPetPublicId2, owner.Pets[1].Id);
            Assert.AreEqual(DefaultPetName2, owner.Pets[1].Name);
        }

        [Test]
        public async Task CreateAsync_HappyPath_CreatesOwner()
        {
            var client = CreateOwnersClient();

            var response = await client.CreateAsync(new CreateOwnerRequest
            {
                FirstName = DefaultFirstName,
                LastName = DefaultLastName,
            });

            Assert.AreEqual(DefaultFirstName, response.FirstName);
            Assert.AreEqual(DefaultLastName, response.LastName);
            Assert.AreEqual(0, response.Pets.Count);

            using (var verificationContext = CreateDataContext())
            {
                var ownerEntity = await verificationContext.Owners.Where(x => x.PublicId == response.Id).SingleOrDefaultAsync();

                Assert.IsNotNull(ownerEntity);
                Assert.AreEqual(DefaultFirstName, ownerEntity.FirstName);
                Assert.AreEqual(DefaultLastName, ownerEntity.LastName);

                Assert.AreEqual(0, await verificationContext.Pets.CountAsync());
            }
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

        protected async Task CleanupDatabase()
        {
            using var cleanupContext = CreateDataContext();

            cleanupContext.Pets.RemoveRange(await cleanupContext.Pets.ToListAsync());
            cleanupContext.Owners.RemoveRange(await cleanupContext.Owners.ToListAsync());

            await cleanupContext.SaveChangesAsync();
        }

        protected DataContext CreateDataContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=LearningTerraform;Integrated Security=true")
                .Options;

            return new DataContext(dbContextOptions);
        }

        protected HttpClient CreateHttpClient()
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
