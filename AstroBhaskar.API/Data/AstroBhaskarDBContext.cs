using AstroBhaskar.API.Models;
using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Data
{
    public class AstroBhaskarDbContext : AuditDbContext
    {
        public AstroBhaskarDbContext()
        {

        }

        public AstroBhaskarDbContext(DbContextOptions<AstroBhaskarDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AstroUser>()
                .HasOne(a => a.UserPermission)
                .WithOne(a => a.AstroUser)
                .HasForeignKey<UserPermission>(c => c.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var _connectionString = "server=.;Database=AstroBhashkar;Integrated Security=true;";
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<AstroUser> AstroUsers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<MasterCollection> MasterCollections { get; set; }
        public DbSet<FirebaseToken> FirebaseTokens { get; set; }
        public DbSet<SubscribeNewsLetter> SubscribeNewsLetters { get; set; }

        public override int SaveChanges()
        {
            AddDateTimeStamp();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDateTimeStamp();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddDateTimeStamp()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                var hasChange = entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified;
                if (hasChange)
                {
                    if (!(entityEntry.Entity is BaseModel baseModel)) continue;
                    var now = DateTime.Now;
                    if (entityEntry.State == EntityState.Added)
                    {
                        baseModel.CreatedAt = now;
                    }
                    else
                    {
                        baseModel.UpdatedAt = now;
                    }
                }
            }
        }

    }
}
