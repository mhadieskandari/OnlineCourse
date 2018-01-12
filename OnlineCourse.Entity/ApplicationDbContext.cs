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

        public DbSet<History> Histories { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentDetails> EnrollmentsDetails { get; set; }
        public DbSet<Course> Courses { get; set; }
    }



}
