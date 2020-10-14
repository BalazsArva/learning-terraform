using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById
{
    public class GetOwnerByIdQueryHandler : IGetOwnerByIdQueryHandler
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public GetOwnerByIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task<Owner> HandleAsync(GetOwnerByIdQuery query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using var uow = unitOfWorkFactory.Create();

            return await uow.OwnerReadRepository.GetByIdAsync(query.Id);
        }
    }
}
