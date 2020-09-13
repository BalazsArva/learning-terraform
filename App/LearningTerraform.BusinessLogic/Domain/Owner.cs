using System.Collections.Generic;

namespace LearningTerraform.BusinessLogic.Domain
{
    public class Owner
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Pet> Pets { get; } = new List<Pet>();
    }
}
