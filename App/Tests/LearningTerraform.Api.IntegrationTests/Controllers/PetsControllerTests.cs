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
    public class PetsControllerTests : IntegrationTestsBase
    {
        [SetUp]
        public async Task SetUpAsync()
        {
            await CleanupDatabaseAsync();
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        [Category(TestExecutionPolicies.PostDeploymentProd)]
        public void GetByIdAsync_PetDoesNotExist_ReturnsNotFound()
        {
            var client = CreatePetsClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.GetByIdAsync(DefaultNonexistentEntityId));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        public async Task GetByIdAsync_PetExists_ReturnsPet()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultOwnerFirstName,
                    LastName = DefaultOwnerLastName,
                    PublicId = DefaultOwnerPublicId,
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
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        [Category(TestExecutionPolicies.PostDeploymentProd)]
        public void CreateAsync_CreatingPetForNonexistentOwner_ReturnsNotFound()
        {
            var client = CreatePetsClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.CreateAsync(DefaultOwnerPublicId, new CreatePetRequest()));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        public async Task CreateAsync_HappyPath_CreatesPet()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultOwnerFirstName,
                    LastName = DefaultOwnerLastName,
                    PublicId = DefaultOwnerPublicId,
                };

                preparationContext.Owners.Add(ownerDbEntity);

                await preparationContext.SaveChangesAsync();
            }

            var client = CreatePetsClient();

            var response = await client.CreateAsync(
                DefaultOwnerPublicId,
                new CreatePetRequest { Name = DefaultPetName1 });

            Assert.AreEqual(DefaultPetName1, response.Name);
            Assert.IsTrue(Guid.TryParseExact(response.Id, "n", out var _), "Expected the created pet Id to be a valid normalized GUID.");

            using (var verificationContext = CreateDataContext())
            {
                var petEntity = await verificationContext
                    .Pets
                    .Where(x => x.PublicId == response.Id && x.Owner.PublicId == DefaultOwnerPublicId)
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
