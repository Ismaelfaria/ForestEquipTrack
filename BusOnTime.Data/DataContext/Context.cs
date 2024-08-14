using BusOnTime.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BusOnTime.Data.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<EquipmentModelStateHourlyEarnings> EquipmentModelStateHourlyEarnings { get; set; }
        public DbSet<EquipmentState> EquipmentState { get; set; }
        public DbSet<EquipmentModel> EquipmentModel { get; set; }
        public DbSet<EquipmentStateHistory> EquipmentStateHistory { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentPositionHistory> EquipmentPositionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de Equipment e EquipmentModel
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.EquipmentModel)
                .WithMany(m => m.Equipment)
                .HasForeignKey(e => e.EquipmentModelId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de EquipmentModel e EquipmentModelStateHourlyEarnings
            modelBuilder.Entity<EquipmentModelStateHourlyEarnings>()
                .HasKey(em => new { em.EquipmentModelId, em.EquipmentStateId });

            modelBuilder.Entity<EquipmentModelStateHourlyEarnings>()
                .HasOne(em => em.EquipmentModel)
                .WithMany(m => m.EquipmentModelStateHourlyEarnings)
                .HasForeignKey(em => em.EquipmentModelId);

            modelBuilder.Entity<EquipmentModelStateHourlyEarnings>()
                .HasOne(em => em.EquipmentState)
                .WithMany(s => s.EquipmentModelStateHourlyEarnings)
                .HasForeignKey(em => em.EquipmentStateId);

            // Configuração de EquipmentState e EquipmentStateHistory
            modelBuilder.Entity<EquipmentStateHistory>()
                .HasKey(e => new { e.EquipmentId, e.Date });

            modelBuilder.Entity<EquipmentStateHistory>()
                .HasOne(e => e.Equipment)
                .WithMany(eq => eq.EquipmentStateHistories)
                .HasForeignKey(e => e.EquipmentId);

            modelBuilder.Entity<EquipmentStateHistory>()
                .HasOne(e => e.EquipmentState)
                .WithMany(s => s.EquipmentStateHistories)
                .HasForeignKey(e => e.EquipmentStateId);

            // Configuração de Equipment e EquipmentPositionHistory
            modelBuilder.Entity<EquipmentPositionHistory>()
                .HasKey(e => new { e.EquipmentId, e.Date });

            modelBuilder.Entity<EquipmentPositionHistory>()
                .HasOne(e => e.Equipment)
                .WithMany(eq => eq.EquipmentPositionHistories)
                .HasForeignKey(e => e.EquipmentId);

        }
    }
}
