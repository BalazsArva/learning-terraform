using System;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstract;
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

        public async Task<string> CreateAsync(string ownerId, Pet pet)
        {
            var ownerEntity = await context.Owners.FirstOrDefaultAsync(x => x.PublicId == ownerId);

            var petEntity = new Entities.Pet
            {
                Name = pet.Name,
                OwnerId = ownerEntity.Id,
                PublicId = Guid.NewGuid().ToString("n"),
            };

            context.Pets.Add(petEntity);

            return petEntity.PublicId;
        }

        public async Task<Pet> GetByIdAsync(string id)
        {
            var entity = await context.Pets.FindAsync(id);

            if (entity is null)
            {
                return null;
            }

            return new Pet
            {
                Id = entity.PublicId,
                Name = entity.Name,
            };
        }
    }
}
