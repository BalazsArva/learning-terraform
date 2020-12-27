using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearningTerraform.DataAccess.MsSql.Database
{
    public class PetRepository : IPetReadRepository, IPetWriteRepository
    {
        private readonly DataContext context;

        public PetRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> CreateAsync(CreatePetDto pet)
        {
            if (pet is null)
            {
                throw new ArgumentNullException(nameof(pet));
            }

            var ownerEntity = await context.Owners.AsNoTracking().FirstOrDefaultAsync(x => x.PublicId == pet.OwnerId);
            var petEntity = new Entities.Pet(ownerEntity.Id, Guid.NewGuid().ToString("n"), pet.Name);

            context.Pets.Add(petEntity);

            return petEntity.PublicId;
        }

        public async Task<Pet> GetByIdAsync(string id)
        {
            var entity = await context.Pets.AsNoTracking().FirstOrDefaultAsync(x => x.PublicId == id);

            if (entity is null)
            {
                return null;
            }

            return new(entity.PublicId, entity.Name);
        }
    }
}
