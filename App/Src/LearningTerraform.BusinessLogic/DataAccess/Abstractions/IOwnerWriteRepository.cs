using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IOwnerWriteRepository
    {
        Task<string> CreateAsync(CreateOwnerDto owner);
    }
}
