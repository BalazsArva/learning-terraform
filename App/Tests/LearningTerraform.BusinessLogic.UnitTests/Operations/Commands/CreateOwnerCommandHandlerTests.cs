using System;
using System.Linq;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner;
using Moq;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Commands
{
    [TestFixture]
    public class CreateOwnerCommandHandlerTests : OperationsTestsBase
    {
        private const string DefaultFirstName = "DefaultFirstName";
        private const string DefaultLastName = "DefaultLastName";

        private CreateOwnerCommandHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new CreateOwnerCommandHandler(UnitOfWorkFactoryMock.Object);
        }

        [Test]
        public void HandleAsync_CommandIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.HandleAsync(null));
        }

        [Test]
        public async Task HandleAsync_HappyPath_StoresOwner()
        {
            var command = new CreateOwnerCommand
            {
                FirstName = DefaultFirstName,
                LastName = DefaultLastName,
            };

            var ownerId = await handler.HandleAsync(command);

            var allOwners = OwnerRepositoryMock.Object.Owners;

            var ownerIsInRepository = allOwners.Any(x =>
                string.Equals(DefaultFirstName, x.FirstName, StringComparison.Ordinal) &&
                string.Equals(DefaultLastName, x.LastName, StringComparison.Ordinal) &&
                string.Equals(ownerId, x.Id, StringComparison.Ordinal));

            Assert.IsTrue(ownerIsInRepository, "Expected to find the new owner in the repository");
            Assert.AreEqual(1, allOwners.Count());

            UnitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
