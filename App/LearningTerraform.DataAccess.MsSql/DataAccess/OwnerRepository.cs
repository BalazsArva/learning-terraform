using System;
using System.Linq;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstract;
using LearningTerraform.BusinessLogic.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearningTerraform.DataAccess.MsSql.DataAccess
{
    public class OwnerRepository : IOwnerReadRepository, IOwnerWriteRepository
    {
        private readonly DataContext context;

        public OwnerRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<string> CreateAsync(Owner owner)
        {
            var entity = new Entities.Owner
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                PublicId = Guid.NewGuid().ToString("n"),
            };

            context.Owners.Add(entity);

            return Task.FromResult(entity.PublicId);
        }

        public async Task<Owner> GetByIdAsync(string id)
        {
            var entity = await context.Owners.FindAsync(id);

            if (entity is null)
            {
                return null;
            }

            var petEntities = await context
                .Pets
                .Where(x => x.OwnerId == entity.Id)
                .ToListAsync();

            var pets = petEntities.Select(x => new Pet
            {
                Id = x.PublicId,
                Name = x.Name,
            });

            var result = new Owner
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Id = entity.PublicId,
            };

            result.Pets.AddRange(pets);

            return result;
        }
    }
}
