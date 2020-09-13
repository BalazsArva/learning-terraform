using LearningTerraform.DataAccess.MsSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningTerraform.DataAccess.MsSql.DataAccess
{
    public class DataContext : DbContext
    {
        // Length of a GUID without dashes
        private const int NormalizedGuidLength = 32;

        public DataContext(DbContextOptions<DataContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigurePets(modelBuilder);
            ConfigureOwners(modelBuilder);
        }

        private void ConfigurePets(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pet>()
                .Property(x => x.Id)
                .UseIdentityColumn();

            modelBuilder
                .Entity<Pet>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Pet>()
                .Property(x => x.PublicId)
                .IsRequired()
                .HasMaxLength(NormalizedGuidLength);

            modelBuilder
                .Entity<Pet>()
                .HasIndex(x => x.PublicId)
                .IsUnique();

            modelBuilder
                .Entity<Pet>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder
                .Entity<Pet>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Pets)
                .HasForeignKey(x => x.OwnerId);
        }

        private void ConfigureOwners(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Owner>()
                .Property(x => x.Id)
                .UseIdentityColumn();

            modelBuilder
                .Entity<Owner>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Owner>()
                .Property(x => x.PublicId)
                .IsRequired()
                .HasMaxLength(NormalizedGuidLength);

            modelBuilder
                .Entity<Owner>()
                .HasIndex(x => x.PublicId)
                .IsUnique();

            modelBuilder
                .Entity<Owner>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder
                .Entity<Owner>()
                .Property(x => x.LastName)
                .IsRequired();
        }
    }
}
