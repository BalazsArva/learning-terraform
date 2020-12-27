using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IPetWriteRepository
    {
        Task<string> CreateAsync(CreatePetDto pet);
    }
}
