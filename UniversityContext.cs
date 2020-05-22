using CosmosEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosEfCore
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
            .Property(s => s.College)
            .HasConversion<string>()
            .HasMaxLength(50);

            modelBuilder.Entity<Staff>()
            .Property(s => s.ContractType)
            .HasConversion<string>()
            .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .HasPartitionKey(p => p.College);

            modelBuilder.Entity<Staff>()
                .HasPartitionKey(p => p.College);

            modelBuilder.Entity<Administrative>()
                .HasPartitionKey(p => p.College);

            modelBuilder.Entity<Lecturer>()
                .HasPartitionKey(p => p.College);

            modelBuilder.Entity<Student>()
                .HasPartitionKey(p => p.College);
        }
    }
}
