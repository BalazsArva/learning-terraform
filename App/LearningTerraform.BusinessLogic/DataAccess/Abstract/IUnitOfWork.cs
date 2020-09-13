using System;
using System.Threading.Tasks;

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IPetReadRepository PetReadRepository { get; }

        IPetWriteRepository PetWriteRepository { get; }

        IOwnerReadRepository OwnerReadRepository { get; }

        IOwnerWriteRepository OwnerWriteRepository { get; }

        Task SaveChangesAsync();
    }
}
