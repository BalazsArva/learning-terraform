namespace LearningTerraform.DataAccess.MsSql.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        public string PublicId { get; set; }

        public int OwnerId { get; set; }

        public string Name { get; set; }

        public virtual Owner Owner { get; }
    }
}
