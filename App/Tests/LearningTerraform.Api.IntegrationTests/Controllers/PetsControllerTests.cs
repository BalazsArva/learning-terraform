using System;
using System.Linq;
using System.Threading.Tasks;
using LearningTerraform.ApiClient;
using LearningTerraform.DataAccess.MsSql.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LearningTerraform.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class PetsControllerTests : ControllerTestBase
    {
        private const string DefaultPublicId = "c6aa50d20a57439ea027df37f9b34a54";
        private const string DefaultFirstName = "OwnerFirstName";
        private const string DefaultLastName = "OwnerLastName";

        private const string DefaultPetName1 = "DefaultPetName1";
        private const string DefaultPetPublicId1 = "6cee7d445bc941a78b58022c115107a8";

        [SetUp]
        public async Task SetUpAsync()
        {
            await CleanupDatabase();
        }

        [Test]
        public void GetByIdAsync_PetDoesNotExist_ReturnsNotFound()
        {
            var client = CreatePetsClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.GetByIdAsync(DefaultNonexistentEntityId));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        public async Task GetByIdAsync_PetExists_ReturnsPet()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultFirstName,
                    LastName = DefaultLastName,
                    PublicId = DefaultPublicId,
                };

                var petEntity = new Pet
                {
                    Name = DefaultPetName1,
                    PublicId = DefaultPetPublicId1,
                };

                ownerDbEntity.Pets.Add(petEntity);

                preparationContext.Owners.Add(ownerDbEntity);
                preparationContext.Pets.Add(petEntity);

                await preparationContext.SaveChangesAsync();
            }

            var client = CreatePetsClient();

            var pet = await client.GetByIdAsync(DefaultPetPublicId1);

            Assert.IsNotNull(pet);

            Assert.AreEqual(DefaultPetPublicId1, pet.Id);
            Assert.AreEqual(DefaultPetName1, pet.Name);
        }

        [Test]
        public void CreateAsync_CreatingPetForNonexistentOwner_ReturnsNotFound()
        {
            var client = CreatePetsClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.CreateAsync(DefaultPublicId, new CreatePetRequest()));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        public async Task CreateAsync_HappyPath_CreatesPet()
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

            var client = CreatePetsClient();

            var response = await client.CreateAsync(
                DefaultPublicId,
                new CreatePetRequest { Name = DefaultPetName1 });

            Assert.AreEqual(DefaultPetName1, response.Name);
            Assert.IsTrue(Guid.TryParseExact(response.Id, "n", out var _), "Expected the created pet Id to be a valid normalized GUID.");

            using (var verificationContext = CreateDataContext())
            {
                var petEntity = await verificationContext
                    .Pets
                    .Where(x => x.PublicId == response.Id && x.Owner.PublicId == DefaultPublicId)
                    .SingleOrDefaultAsync();

                Assert.IsNotNull(petEntity);
                Assert.AreEqual(DefaultPetName1, petEntity.Name);

                Assert.AreEqual(1, await verificationContext.Pets.CountAsync());
            }
        }

        private PetsClient CreatePetsClient()
        {
            return new PetsClient(Factory.ClientOptions.BaseAddress.ToString(), CreateHttpClient());
        }
    }
}
