using System.Threading.Tasks;

namespace LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner
{
    public interface ICreateOwnerCommandHandler
    {
        Task<string> HandleAsync(CreateOwnerCommand command);
    }
}
