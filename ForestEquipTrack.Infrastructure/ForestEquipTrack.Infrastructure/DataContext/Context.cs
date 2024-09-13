using ForestEquipTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForestEquipTrack.Infrastructure.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public DbSet<EquipmentModelStateHourlyEarnings> EquipmentModelStateHourlyEarnings { get; set; }
        public DbSet<EquipmentModel> EquipmentModel { get; set; }
        public DbSet<EquipmentStateHistory> EquipmentStateHistory { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentPositionHistory> EquipmentPositionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.EquipmentId); 

                entity.Property(e => e.Name)
                      .HasMaxLength(100); 

                //Relacionamentos
                entity.HasOne(e => e.EquipmentModel)
                      .WithMany(m => m.Equipment)
                      .HasForeignKey(e => e.EquipmentModelId)
                      .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

                entity.HasMany(e => e.EquipmentStateHistories)
                      .WithOne(eh => eh.Equipment)
                      .HasForeignKey(eh => eh.EquipmentId);

                entity.HasMany(e => e.EquipmentPositionHistories)
                      .WithOne(ep => ep.Equipment)
                      .HasForeignKey(ep => ep.EquipmentId);
            });

            modelBuilder.Entity<EquipmentModel>(entity =>
            {
                entity.HasKey(m => m.EquipmentModelId);

                entity.Property(m => m.Name)
                      .HasMaxLength(100);

                //Relacionamentos
                entity.HasMany(m => m.Equipment)
                      .WithOne(e => e.EquipmentModel)
                      .HasForeignKey(e => e.EquipmentModelId)
                      .IsRequired(false);

                entity.HasMany(m => m.EquipmentModelStateHourlyEarnings)
                      .WithOne(em => em.EquipmentModel)
                      .HasForeignKey(em => em.EquipmentModelId);
            });

            modelBuilder.Entity<EquipmentModelStateHourlyEarnings>(entity =>
            {
                entity.HasKey(em => em.EquipmentModelStateHourlyEarningsId); 

                entity.Property(em => em.Value)
                .HasPrecision(18, 2);

                //Relacionamentos
                entity.HasOne(em => em.EquipmentModel)
                      .WithMany(m => m.EquipmentModelStateHourlyEarnings)
                      .HasForeignKey(em => em.EquipmentModelId)
                      .IsRequired(false);
            });

            modelBuilder.Entity<EquipmentPositionHistory>(entity =>
            {
                entity.HasKey(ep => ep.EquipmentPositionId);

                //Relacionamentos
                entity.HasOne(ep => ep.Equipment)
                      .WithMany(e => e.EquipmentPositionHistories)
                      .HasForeignKey(ep => ep.EquipmentId)
                      .IsRequired(false);
            });

            
            modelBuilder.Entity<EquipmentStateHistory>(entity =>
            {
                entity.HasKey(esh => esh.EquipmentStateHistoryId);


                //Relacionamentos
                entity.HasOne(esh => esh.Equipment)
                      .WithMany(e => e.EquipmentStateHistories)
                      .HasForeignKey(esh => esh.EquipmentId)
                      .IsRequired(false);
            });
        }
    }
}
