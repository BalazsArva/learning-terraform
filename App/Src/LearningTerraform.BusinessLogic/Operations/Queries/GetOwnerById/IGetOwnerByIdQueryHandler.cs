using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById
{
    public interface IGetOwnerByIdQueryHandler
    {
        Task<Owner> HandleAsync(GetOwnerByIdQuery query);
    }
}
