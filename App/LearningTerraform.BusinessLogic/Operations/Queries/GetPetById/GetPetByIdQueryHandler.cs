using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.Operations.Queries.GetPetById
{
    public class GetPetByIdQueryHandler : IGetPetByIdQueryHandler
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public GetPetByIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task<Pet> HandleAsync(GetPetByIdQuery query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using var uow = unitOfWorkFactory.Create();

            return await uow.PetReadRepository.GetByIdAsync(query.Id);
        }
    }
}
