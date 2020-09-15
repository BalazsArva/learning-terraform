using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;

namespace LearningTerraform.DataAccess.MsSql.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private DataContext context;

        private readonly OwnerRepository ownerRepository;
        private readonly PetRepository petRepository;

        public UnitOfWork(DataContext dataContext)
        {
            context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

            ownerRepository = new OwnerRepository(dataContext);
            petRepository = new PetRepository(dataContext);
        }

        public IPetReadRepository PetReadRepository => petRepository;

        public IPetWriteRepository PetWriteRepository => petRepository;

        public IOwnerReadRepository OwnerReadRepository => ownerRepository;

        public IOwnerWriteRepository OwnerWriteRepository => ownerRepository;

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                context = null;
                disposed = true;
            }
        }
    }
}
