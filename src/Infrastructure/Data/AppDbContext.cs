using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext,  IAppDbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext() { }
        protected AppDbContext(DbContextOptions options) : base(options){}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskItemConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ICreatedAndUpdated && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                    ((ICreatedAndUpdated)entityEntry.Entity).Updated = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((ICreatedAndUpdated)entityEntry.Entity).Created = DateTime.Now;
            }
        }
    }
}