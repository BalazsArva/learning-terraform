using System;
using System.Linq;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Exceptions;
using LearningTerraform.BusinessLogic.Operations.Commands.CreatePet;
using Moq;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Commands
{
    [TestFixture]
    public class CreatePetCommandTests : OperationsTestsBase
    {
        private const string DefaultName = "DefaultName";
        private const string DefaultOwnerId = "DefaultOwnerId";

        private CreatePetCommandHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new CreatePetCommandHandler(UnitOfWorkFactoryMock.Object);
        }

        [Test]
        public void HandleAsync_CommandIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.HandleAsync(null));
        }

        [Test]
        public void HandleAsync_OwnerDoesNotExist_ThrowsEntityNotFoundException()
        {
            var command = new CreatePetCommand
            {
                Name = DefaultName,
                OwnerId = DefaultOwnerId,
            };

            var exceptionThrown = Assert.ThrowsAsync<EntityNotFoundException>(async () => await handler.HandleAsync(command));

            var allPets = PetRepositoryMock.Object.Pets;

            Assert.AreEqual(0, allPets.Count());

            PetWriteRepositoryMock.VerifyNoOtherCalls();

            UnitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task HandleAsync_HappyPath_StoresPet()
        {
            var command = new CreatePetCommand
            {
                Name = DefaultName,
                OwnerId = DefaultOwnerId,
            };

            OwnerRepositoryMock.Object.Add(new Domain.Owner { Id = DefaultOwnerId });

            var petId = await handler.HandleAsync(command);

            var allPets = PetRepositoryMock.Object.Pets;

            var petIsInRepository = allPets.Any(x =>
                string.Equals(DefaultName, x.Name, StringComparison.Ordinal) &&
                string.Equals(petId, x.Id, StringComparison.Ordinal));

            Assert.IsTrue(petIsInRepository, "Expected to find the new pet in the repository");
            Assert.AreEqual(1, allPets.Count());

            UnitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
