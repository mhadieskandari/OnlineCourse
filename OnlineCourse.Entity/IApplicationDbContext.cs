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
    }
}
