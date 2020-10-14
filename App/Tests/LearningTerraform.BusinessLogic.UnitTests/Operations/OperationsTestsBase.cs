using System.Collections.Generic;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;
using LearningTerraform.BusinessLogic.UnitTests.Stubs;
using Moq;

namespace LearningTerraform.BusinessLogic.UnitTests.Operations
{
    public abstract class OperationsTestsBase
    {
        protected OperationsTestsBase()
        {
        }

        public Mock<IUnitOfWorkFactory> UnitOfWorkFactoryMock { get; set; }

        public Mock<IUnitOfWork> UnitOfWorkMock { get; set; }

        public Mock<IPetReadRepository> PetReadRepositoryMock { get; set; }

        public Mock<IPetWriteRepository> PetWriteRepositoryMock { get; set; }

        public Mock<StubPetRepository> PetRepositoryMock { get; set; }

        public Mock<IOwnerWriteRepository> OwnerWriteRepositoryMock { get; set; }

        public Mock<IOwnerReadRepository> OwnerReadRepositoryMock { get; set; }

        public Mock<StubOwnerRepository> OwnerRepositoryMock { get; set; }

        public virtual void SetUp()
        {
            var pets = new Dictionary<string, Pet>();
            var owners = new Dictionary<string, Owner>();

            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();

            PetRepositoryMock = new Mock<StubPetRepository>(pets) { CallBase = true };
            PetReadRepositoryMock = PetRepositoryMock.As<IPetReadRepository>();
            PetWriteRepositoryMock = PetRepositoryMock.As<IPetWriteRepository>();

            OwnerRepositoryMock = new Mock<StubOwnerRepository>(owners, pets) { CallBase = true };
            OwnerReadRepositoryMock = OwnerRepositoryMock.As<IOwnerReadRepository>();
            OwnerWriteRepositoryMock = OwnerRepositoryMock.As<IOwnerWriteRepository>();

            UnitOfWorkMock
                .SetupGet(x => x.PetReadRepository)
                .Returns(PetReadRepositoryMock.Object);

            UnitOfWorkMock
                .SetupGet(x => x.PetWriteRepository)
                .Returns(PetWriteRepositoryMock.Object);

            UnitOfWorkMock
                .SetupGet(x => x.OwnerReadRepository)
                .Returns(OwnerReadRepositoryMock.Object);

            UnitOfWorkMock
                .SetupGet(x => x.OwnerWriteRepository)
                .Returns(OwnerWriteRepositoryMock.Object);

            UnitOfWorkFactoryMock
                .Setup(x => x.Create())
                .Returns(UnitOfWorkMock.Object);
        }
    }
}
