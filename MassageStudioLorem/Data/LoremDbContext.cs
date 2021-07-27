namespace MassageStudioLorem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using MassageStudioLorem.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class LoremDbContext : IdentityDbContext
    {
        public LoremDbContext(DbContextOptions<LoremDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Client> Clients { get; set; }

        //public DbSet<ApplicationUser> IdentityUsers { get; set; }

        public DbSet<Massage> Massages { get; set; }

        public DbSet<Masseur> Masseurs { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<MasseurAvailableHours> MasseursAvailableHours
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Masseur>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Masseur>(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Client>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Appointment>()
                .HasOne<Masseur>()
                .WithMany(x => x.WorkSchedule)
                .HasForeignKey(x => x.MasseurId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Appointment>()
            //    .HasOne<Client>()
            //    .WithMany(x => x.Appointments)
            //    .HasForeignKey(x => x.ClientId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Appointment>()
                .HasOne<Massage>()
                .WithOne()
                .HasForeignKey<Appointment>(x => x.MassageId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}