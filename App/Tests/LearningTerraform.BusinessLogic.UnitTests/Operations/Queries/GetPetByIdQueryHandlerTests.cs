using LearningTerraform.BusinessLogic.Operations.Queries.GetPetById;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Queries
{
    [TestFixture]
    public class GetPetByIdQueryHandlerTests : OperationsTestsBase
    {
        private GetPetByIdQueryHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new GetPetByIdQueryHandler(UnitOfWorkFactoryMock.Object);
        }
    }
}
