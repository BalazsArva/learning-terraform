using LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Commands
{
    [TestFixture]
    public class CreateOwnerCommandTests : OperationsTestsBase
    {
        private CreateOwnerCommandHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new CreateOwnerCommandHandler(UnitOfWorkFactoryMock.Object);
        }
    }
}
