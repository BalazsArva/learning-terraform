using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstract;

namespace LearningTerraform.BusinessLogic.Commands.CreateOwner
{
    public class CreateOwnerCommandHandler : ICreateOwnerCommandHandler
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public CreateOwnerCommandHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task<string> HandleAsync(CreateOwnerCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            using var uow = unitOfWorkFactory.Create();

            var ownerId = await uow.OwnerWriteRepository.CreateAsync(new Domain.Owner
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
            });

            await uow.SaveChangesAsync();

            return ownerId;
        }
    }
}
