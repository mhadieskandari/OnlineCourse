using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Entity
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { set; get; }
        public DbSet<Gallery> Galleries { set; get; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassRoomDetails> ClassRoomDetails { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Present>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Present).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(c => c.ClassRoomDetails)
                .WithOne(e => e.Student).OnDelete(DeleteBehavior.Restrict);


        }

    }


}
