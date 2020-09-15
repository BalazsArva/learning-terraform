using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.Operations.Queries.GetPetById
{
    public interface IGetPetByIdQueryHandler
    {
        Task<Pet> HandleAsync(GetPetByIdQuery query);
    }
}