namespace FindMydinner.Domain.Model.Entities
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public partial class FindMyDinnerContext : DbContext
    {
        public FindMyDinnerContext()
        {
        }

        public FindMyDinnerContext(DbContextOptions<FindMyDinnerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=INBLRWIT241492\\SQLEXPRESS;Database=FindMyDinner;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
