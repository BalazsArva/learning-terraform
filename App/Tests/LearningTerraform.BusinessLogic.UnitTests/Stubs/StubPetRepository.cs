using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningTerraform.BusinessLogic.DataAccess.Abstractions;
using LearningTerraform.BusinessLogic.Domain;

namespace LearningTerraform.BusinessLogic.UnitTests.Stubs
{
    public class StubPetRepository : IPetReadRepository, IPetWriteRepository
    {
        private readonly Dictionary<string, Pet> pets;

        public StubPetRepository(Dictionary<string, Pet> pets)
        {
            this.pets = pets ?? throw new ArgumentNullException(nameof(pets));
        }

        public IEnumerable<Pet> Pets => pets.Values;

        public void Add(Pet pet)
        {
            pets[pet.Id] = pet;
        }

        public Task<string> CreateAsync(CreatePetDto pet)
        {
            var id = Guid.NewGuid().ToString("n");

            pets[id] = new Pet(id, pet.Name);

            return Task.FromResult(id);
        }

        public Task<Pet> GetByIdAsync(string id)
        {
            var result = pets.TryGetValue(id, out var pet)
                ? pet
                : default;

            return Task.FromResult(result);
        }
    }
}
