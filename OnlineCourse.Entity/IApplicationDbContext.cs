using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Entity
{

    public interface IApplicationDbContext
    {
        DbSet<User> Users { set; get; }
        DbSet<Gallery> Galleries { set; get; }
        DbSet<History> Histories { get; set; }
        DbSet<Term> Terms { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Schedule> Schedules { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Enrollment> Enrollments { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<ClassRoomDetails> ClassRoomDetails { get; set; }
        DbSet<ClassRoom> ClassRooms { get; set; }
        DbSet<Present> Presents { get; set; }
    }
}
