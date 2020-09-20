using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Queries
{
    [TestFixture]
    public class GetOwnerByIdQueryHandlerTests : OperationsTestsBase
    {
        private const string DefaultId = "DefaultId";

        private GetOwnerByIdQueryHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new GetOwnerByIdQueryHandler(UnitOfWorkFactoryMock.Object);
        }

        [Test]
        public void HandleAsync_QueryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.HandleAsync(null));
        }

        [Test]
        public async Task HandleAsync_OwnerDoesNotExist_ReturnsNull()
        {
            var result = await handler.HandleAsync(new GetOwnerByIdQuery { Id = DefaultId, });

            Assert.IsNull(result);
        }

        [Test]
        public async Task HandleAsync_OwnerExists_ReturnsOwner()
        {
            OwnerRepositoryMock.Object.Add(new Domain.Owner { Id = DefaultId, });

            var result = await handler.HandleAsync(new GetOwnerByIdQuery { Id = DefaultId });

            Assert.AreEqual(DefaultId, result.Id);
        }
    }
}
