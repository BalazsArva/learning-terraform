using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.Queries.GetOwnerById
{
    public interface IGetOwnerByIdQueryHandler
    {
        Task<Owner> HandleAsync(GetOwnerByIdQuery query);
    }
}