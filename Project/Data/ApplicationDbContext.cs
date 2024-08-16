using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.ViewModels;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PigeonDTO> Pigeons { get; set; }
        //public DbSet<PigeonRelation> PigeonRelations { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PigeonDTO>()
        //        .HasOne(p => p.FatherPigeon)  // Один батько
        //        .WithMany()                    // Може мати багато дітей
        //        .HasForeignKey(p => p.Father); // Зовнішній ключ

        //    // Додаткові налаштування можуть бути додані тут

        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
