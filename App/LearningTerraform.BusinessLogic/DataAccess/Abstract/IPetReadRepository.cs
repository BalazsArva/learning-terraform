using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IPetReadRepository
    {
        Task<Pet> GetByIdAsync(string id);
    }
}
