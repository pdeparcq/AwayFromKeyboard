using AwayFromKeyboard.Domain.Meta;
using Microsoft.EntityFrameworkCore;

namespace AwayFromKeyboard.Test
{
    public class MetaDbContext : DbContext
    {

        public MetaDbContext(DbContextOptions<MetaDbContext> options) : base(options)
        {
        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<BaseType> Types { get; set; }
        public DbSet<AggregateRoot> AggregateRoots { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<ValueObject> ValueObjects { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Module>()
                .HasMany(m => m.SubModules)
                .WithOne(m => m.ParentModule)
                .HasForeignKey(m => m.ParentModuleId)
                .IsRequired(false);

            modelBuilder.Entity<BaseType>()
                .HasMany(t => t.Properties)
                .WithOne(p => p.ParentType)
                .HasForeignKey(p => p.ParentTypeId)
                .IsRequired();

            modelBuilder.Entity<Property>()
                .HasKey(p => new
                {
                    p.ParentTypeId,
                    p.Name
                });

            modelBuilder.Entity<Property>()
                .HasOne(p => p.ValueType)
                .WithMany()
                .HasForeignKey(p => p.ValueTypeId)
                .IsRequired(false);

            modelBuilder.Entity<Entity>()
                .HasOne(e => e.Identity)
                .WithMany()
                .HasForeignKey(e => e.IdentityId)
                .IsRequired();

            modelBuilder.Entity<Entity>()
                .HasMany(e => e.Relations)
                .WithOne(r => r.FromEntity)
                .HasForeignKey(r => r.FromEntityId)
                .IsRequired();

            modelBuilder.Entity<EntityRelation>()
                .HasKey(r => new
                {
                    r.Name,
                    r.FromEntityId,
                    r.ToEntityId
                });

            modelBuilder.Entity<EntityRelation>()
                .HasOne(r => r.ToEntity)
                .WithMany()
                .HasForeignKey(r => r.ToEntityId)
                .IsRequired();
        }
    }
}
