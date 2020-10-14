using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Operations.Queries.GetPetById;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Queries
{
    [TestFixture]
    public class GetPetByIdQueryHandlerTests : OperationsTestsBase
    {
        private const string DefaultId = "DefaultId";

        private GetPetByIdQueryHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new GetPetByIdQueryHandler(UnitOfWorkFactoryMock.Object);
        }

        [Test]
        public void HandleAsync_QueryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.HandleAsync(null));
        }

        [Test]
        public async Task HandleAsync_PetDoesNotExist_ReturnsNull()
        {
            var result = await handler.HandleAsync(new GetPetByIdQuery { Id = DefaultId });

            Assert.IsNull(result);
        }

        [Test]
        public async Task HandleAsync_PetExists_ReturnsPet()
        {
            PetRepositoryMock.Object.Add(new Domain.Pet { Id = DefaultId });

            var result = await handler.HandleAsync(new GetPetByIdQuery { Id = DefaultId });

            Assert.AreEqual(DefaultId, result.Id);
        }
    }
}
