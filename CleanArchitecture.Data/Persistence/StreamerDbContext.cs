using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContext : DbContext
{
    public StreamerDbContext(DbContextOptions<StreamerDbContext> dbContextOptions) 
        : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(m => m.Streamer)
            .HasForeignKey(m => m.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(p => p.Actors)
            .WithMany(t => t.Videos)
            .UsingEntity<VideoActor>(
                pt => pt.HasKey(e => new { e.ActorId, e.VideoId });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entity in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Entity.CreatedBy = "system";
                    entity.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entity.Entity.LastModifiedDate = DateTime.Now;
                    entity.Entity.LastModifiedBy = "system";
                    break;

            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Streamer>? Streamers { get; set; }

    public DbSet<Video>? Videos { get; set; }

    public DbSet<Actor>? Actores { get; set; }

    public DbSet<Director>? Directores { get; set; }

}
