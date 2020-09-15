using System.Threading.Tasks;

namespace LearningTerraform.BusinessLogic.Operations.Commands.CreatePet
{
    public interface ICreatePetCommandHandler
    {
        Task<string> HandleAsync(CreatePetCommand command);
    }
}