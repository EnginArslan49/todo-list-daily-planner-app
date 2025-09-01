
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using toDoList.Models;

namespace toDoList.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tablo adını ve varsayılan değerleri ayarla
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks"); // Veritabanındaki tablo adı
                entity.Property(t => t.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.IsCompleted).HasDefaultValue(false);
            });
        }



    }
}
