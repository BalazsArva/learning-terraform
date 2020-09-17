using System.Collections.Generic;

namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        public string PublicId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Pet> Pets { get; } = new List<Pet>();
    }
}
