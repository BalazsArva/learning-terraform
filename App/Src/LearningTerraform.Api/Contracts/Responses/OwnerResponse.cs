using System.Collections.Generic;

namespace LearningTerraform.Api.Contracts.Responses
{
    public class OwnerResponse
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<PetResponse> Pets { get; } = new List<PetResponse>();
    }
}
