using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        
        Task<User> GetAsync(int id);

        User Get(string email, string password);
        IEnumerable<User> GetByEmail(string email);
        User GetByMobile(string mobile);
        void Add(User user);
        void Update(User user);
        void Remove(int id);
        bool IsExist(int id);
        bool IsExistEmail(string email);
        bool IsExistMobile(string mobile);
        byte? GetAccessable(string email);
        bool IsCorrectEmailPassword(string email, string password);

        byte? GetStateAccount(string email, string password);
        bool CanDelete(int id);
        Task<User> GetByEmailAsync(string email);
        Task<int> GetIdByEmailAsync(string email);

        Task<bool> CanDeleteAsync(int id);
        Task<bool> ValidateLastChanged(string email, string lastChanged);
    }
}
