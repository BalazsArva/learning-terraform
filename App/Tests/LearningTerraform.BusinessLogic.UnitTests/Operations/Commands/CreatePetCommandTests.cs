using LearningTerraform.BusinessLogic.Operations.Commands.CreatePet;
using NUnit.Framework;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations.Commands
{
    [TestFixture]
    public class CreatePetCommandTests : OperationsTestsBase
    {
        private CreatePetCommandHandler handler;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            handler = new CreatePetCommandHandler(UnitOfWorkFactoryMock.Object);
        }
    }
}
