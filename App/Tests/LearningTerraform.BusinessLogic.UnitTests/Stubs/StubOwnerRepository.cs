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

        public void Add(Owner owner)
        {
            owners[owner.Id] = owner;
        }

        public Task<string> CreateAsync(CreateOwnerDto owner)
        {
            var id = Guid.NewGuid().ToString("n");

            owners[id] = new Owner(id, owner.FirstName, owner.LastName, Array.Empty<Pet>());

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
