using System.Collections.Generic;

namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public class Owner
    {
        public int Id { get; init; }

        public string PublicId { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public virtual ICollection<Pet> Pets { get; } = new List<Pet>();
    }
}
