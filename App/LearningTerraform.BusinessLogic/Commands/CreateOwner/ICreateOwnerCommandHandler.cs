using System.Threading.Tasks;

namespace LearningTerraform.BusinessLogic.Commands.CreateOwner
{
    public interface ICreateOwnerCommandHandler
    {
        Task<string> HandleAsync(CreateOwnerCommand command);
    }
}