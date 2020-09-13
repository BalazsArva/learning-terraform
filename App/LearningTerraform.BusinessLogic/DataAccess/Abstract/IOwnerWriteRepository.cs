using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IOwnerWriteRepository
    {
        Task<string> CreateAsync(Owner owner);
    }
}
