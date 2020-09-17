using LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Queries
{
    [TestFixture]
    public class GetOwnerByIdQueryTests : OperationsTestsBase
    {
        private GetOwnerByIdQueryHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new GetOwnerByIdQueryHandler(UnitOfWorkFactoryMock.Object);
        }
    }
}
