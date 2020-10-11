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
    public class OwnersControllerTests : ControllerTestsBase
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
        public void GetByIdAsync_OwnerDoesNotExist_ReturnsNotFound()
        {
            var client = CreateOwnersClient();

            var exceptionThrown = Assert.ThrowsAsync<SwaggerException>(
                async () => await client.GetByIdAsync(DefaultNonexistentEntityId));

            Assert.AreEqual(StatusCodes.Status404NotFound, exceptionThrown.StatusCode);
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        public async Task GetByIdAsync_OwnerHasNoPets_ReturnsOwnerWithZeroPets()
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

            var client = CreateOwnersClient();

            var owner = await client.GetByIdAsync(DefaultOwnerPublicId);

            Assert.IsNotNull(owner);

            Assert.AreEqual(DefaultOwnerPublicId, owner.Id);
            Assert.AreEqual(DefaultOwnerFirstName, owner.FirstName);
            Assert.AreEqual(DefaultOwnerLastName, owner.LastName);
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        public async Task GetByIdAsync_OwnerHasPets_ReturnsOwnerWithAllPets()
        {
            using (var preparationContext = CreateDataContext())
            {
                var ownerDbEntity = new Owner
                {
                    FirstName = DefaultOwnerFirstName,
                    LastName = DefaultOwnerLastName,
                    PublicId = DefaultOwnerPublicId,
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

            var owner = await client.GetByIdAsync(DefaultOwnerPublicId);

            Assert.IsNotNull(owner);

            Assert.AreEqual(DefaultOwnerPublicId, owner.Id);
            Assert.AreEqual(DefaultOwnerFirstName, owner.FirstName);
            Assert.AreEqual(DefaultOwnerLastName, owner.LastName);

            Assert.AreEqual(2, owner.Pets.Count);

            Assert.AreEqual(DefaultPetPublicId1, owner.Pets[0].Id);
            Assert.AreEqual(DefaultPetName1, owner.Pets[0].Name);

            Assert.AreEqual(DefaultPetPublicId2, owner.Pets[1].Id);
            Assert.AreEqual(DefaultPetName2, owner.Pets[1].Name);
        }

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        public async Task CreateAsync_HappyPath_CreatesOwner()
        {
            var client = CreateOwnersClient();

            var response = await client.CreateAsync(new CreateOwnerRequest
            {
                FirstName = DefaultOwnerFirstName,
                LastName = DefaultOwnerLastName,
            });

            Assert.AreEqual(DefaultOwnerFirstName, response.FirstName);
            Assert.AreEqual(DefaultOwnerLastName, response.LastName);
            Assert.AreEqual(0, response.Pets.Count);

            using (var verificationContext = CreateDataContext())
            {
                var ownerEntity = await verificationContext.Owners.Where(x => x.PublicId == response.Id).SingleOrDefaultAsync();

                Assert.IsNotNull(ownerEntity);
                Assert.AreEqual(DefaultOwnerFirstName, ownerEntity.FirstName);
                Assert.AreEqual(DefaultOwnerLastName, ownerEntity.LastName);

                Assert.AreEqual(0, await verificationContext.Pets.CountAsync());
            }
        }

        private OwnersClient CreateOwnersClient()
        {
            return new OwnersClient(Factory.ClientOptions.BaseAddress.ToString(), CreateHttpClient());
        }
    }
}
