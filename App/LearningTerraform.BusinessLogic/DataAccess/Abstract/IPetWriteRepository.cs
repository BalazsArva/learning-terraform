using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IPetWriteRepository
    {
        Task<string> CreateAsync(string ownerId, Pet pet);
    }
}
