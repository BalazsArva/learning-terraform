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

            var stubPetRepository = new StubPetRepository(pets);
            var stubOwnerRepository = new StubOwnerRepository(owners, pets);

            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();

            PetRepositoryMock = new Mock<StubPetRepository> { CallBase = true };
            PetReadRepositoryMock = PetRepositoryMock.As<IPetReadRepository>();
            PetWriteRepositoryMock = PetRepositoryMock.As<IPetWriteRepository>();

            OwnerRepositoryMock = new Mock<StubOwnerRepository> { CallBase = true };
            OwnerReadRepositoryMock = OwnerRepositoryMock.As<IOwnerReadRepository>();
            OwnerWriteRepositoryMock = OwnerRepositoryMock.As<IOwnerWriteRepository>();

            // Could also try (if the above does not work):
            // Mock.Get<IPetReadRepository>(stubPetRepository);
            // Mock.Get<IPetWriteRepository>(stubPetRepository);
            // Mock.Get<IOwnerReadRepository>(stubOwnerRepository);
            // Mock.Get<IOwnerWriteRepository>(stubOwnerRepository);
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
