using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IOwnerReadRepository
    {
        Task<Owner> GetByIdAsync(string id);
    }
}
