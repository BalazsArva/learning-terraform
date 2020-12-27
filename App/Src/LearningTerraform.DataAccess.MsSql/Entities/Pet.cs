namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public class Pet
    {
        public int Id { get; init; }

        public string PublicId { get; init; }

        public int OwnerId { get; init; }

        public string Name { get; init; }

        public virtual Owner Owner { get; }
    }
}
