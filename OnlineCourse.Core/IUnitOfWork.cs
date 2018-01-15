using OnlineCourse.Core.Repositories;
using OnlineCourse.Core.Repositories.Interfaces;
using OnlineCourse.Entity.Models;
using System;
using System.Threading.Tasks;

namespace OnlineCourse.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        GenericRepository<User> userRepository { get;}
        
        int Complete();

        Task<int> CompleteAsync();
        
    }
}