using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;

namespace LearningTerraform.BusinessLogic.Operations.Commands.CreatePet
{
    public class CreatePetCommandHandler : ICreatePetCommandHandler
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public CreatePetCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task<string> HandleAsync(CreatePetCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            using var uow = unitOfWorkFactory.Create();

            var petId = await uow.PetWriteRepository.CreateAsync(command.OwnerId, new Domain.Pet
            {
                Name = command.Name,
            });

            await uow.SaveChangesAsync();

            return petId;
        }
    }
}
