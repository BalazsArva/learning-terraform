using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IOwnerReadRepository
    {
        Task<Owner> GetByIdAsync(string id);
    }
}
