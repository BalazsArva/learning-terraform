using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.UnitTests.Stubs
{
    public class StubOwnerRepository : IOwnerReadRepository, IOwnerWriteRepository
    {
        private readonly Dictionary<string, Owner> owners;
        private readonly Dictionary<string, Pet> pets;

        public StubOwnerRepository(Dictionary<string, Owner> owners, Dictionary<string, Pet> pets)
        {
            this.owners = owners ?? throw new ArgumentNullException(nameof(owners));
            this.pets = pets ?? throw new ArgumentNullException(nameof(pets));
        }

        public IEnumerable<Owner> Owners => owners.Values;

        public Task<string> CreateAsync(Owner owner)
        {
            var id = Guid.NewGuid().ToString("n");

            owners[id] = new Owner
            {
                Id = id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
            };

            foreach (var pet in owner.Pets)
            {
                var petEntity = new Pet
                {
                    Name = pet.Name,
                    Id = pet.Id ?? Guid.NewGuid().ToString("n"),
                };

                pets[petEntity.Id] = petEntity;
                owners[id].Pets.Add(petEntity);
            }

            return Task.FromResult(id);
        }

        public Task<bool> ExistsAsync(string id)
        {
            return Task.FromResult(owners.TryGetValue(id, out var owner) && !(owner is null));
        }

        public Task<Owner> GetByIdAsync(string id)
        {
            var result = owners.TryGetValue(id, out var owner)
                ? owner
                : default;

            return Task.FromResult(result);
        }
    }
}
