using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IPetReadRepository
    {
        Task<Pet> GetByIdAsync(string id);
    }
}
