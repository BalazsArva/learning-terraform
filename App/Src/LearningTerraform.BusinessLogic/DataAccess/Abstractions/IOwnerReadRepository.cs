using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IOwnerReadRepository
    {
        Task<bool> ExistsAsync(string id);

        Task<Owner> GetByIdAsync(string id);
    }
}
