using Microsoft.EntityFrameworkCore;
using ContactService.Core.Models;

namespace ContactService.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Person ve ContactInformation tablolarını temsil eder
        public DbSet<Person> People { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Company).HasMaxLength(200);
            });

            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.InfoType).IsRequired();
                entity.Property(c => c.InfoContent).IsRequired().HasMaxLength(255);
                entity.HasOne(c => c.Person)
                      .WithMany(p => p.ContactInformations)
                      .HasForeignKey(c => c.PersonId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
