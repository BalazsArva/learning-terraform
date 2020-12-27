using System.Collections.Generic;

namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public record Owner
    {
        public Owner(string publicId, string firstName, string lastName)
            => (PublicId, FirstName, LastName) = (publicId, firstName, lastName);

        public int Id { get; }

        public string PublicId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public virtual ICollection<Pet> Pets { get; } = new List<Pet>();
    }
}
