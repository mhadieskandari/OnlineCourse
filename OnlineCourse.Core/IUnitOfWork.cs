using OnlineCourse.Core.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineCourse.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}