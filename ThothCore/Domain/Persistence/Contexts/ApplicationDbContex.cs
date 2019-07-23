using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThothCore.Domain.Models;

namespace ThothCore.Domain.Persistence.Contexts
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        #region DbSets

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseUnit> CourseUnits { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Courses

            builder.Entity<Course>().ToTable("Courses");
            builder.Entity<Course>().HasKey(p => p.Id);
            builder.Entity<Course>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Course>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Entity<Course>().HasMany(p => p.Units).WithOne(p => p.Course).HasForeignKey(p => p.CourseId);

            builder.Entity<Course>().HasData(
                new Course {Id = 77, Name = "Test course"}
            );

            #endregion

            #region CourseUnits

            builder.Entity<CourseUnit>().ToTable("CourseUnits");
            builder.Entity<CourseUnit>().HasKey(p => p.Id);
            builder.Entity<CourseUnit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<CourseUnit>().Property(p => p.Name).IsRequired().HasMaxLength(255);

            #endregion



        }
    }
}
