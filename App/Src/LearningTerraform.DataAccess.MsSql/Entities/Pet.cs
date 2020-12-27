namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public record Pet
    {
        public Pet(Owner owner, string publicId, string name)
            => (Owner, PublicId, Name) = (owner, publicId, name);

        public Pet(int ownerId, string publicId, string name)
            => (OwnerId, PublicId, Name) = (ownerId, publicId, name);

        public int Id { get; }

        public string PublicId { get; }

        public int OwnerId { get; }

        public string Name { get; }

        public virtual Owner Owner { get; }
    }
}
