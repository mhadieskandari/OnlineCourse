using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Repositories.Interfaces;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly HistoryService _historyService;

        public UserRepository(IApplicationDbContext context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public User Get(int id)
        {
            try
            {
                return _context.Users.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

        


        public async Task<User> GetAsync(int id)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(m => m.Id == id); ;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }
        public User Get(string email, string password)
        {
            try
            {
                return _context.Users.SingleOrDefault(m => m.Email == email && EncryptDecrypt.Decrypt(m.Password).Equals(password));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }
        public IEnumerable<User> GetByEmail(string email)
        {
            try
            {
                return _context.Users.Where(m => m.Email.Equals(email)).ToList();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.Where(m => m.Email.Equals(email)).SingleOrDefaultAsync();
                return user;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public async Task<int> GetIdByEmailAsync(string email)
        {
            try
            {
                var userId = await _context.Users.Where(m => m.Email.Equals(email)).Select(u => u.Id).SingleOrDefaultAsync();
                return userId;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }
        public User GetByMobile(string mobile)
        {
            try
            {
                var res = _context.Users.FirstOrDefault(m => m.Mobile == mobile);
                return res;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public bool IsExist(int id)
        {
            try
            {
                return _context.Users.Any(m => m.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public bool IsExistEmail(string email)
        {
            try
            {
                return _context.Users.Any(m => m.Email.Equals(email));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public bool IsExistMobile(string mobile)
        {
            try
            {
                var users = _context.Users.Any(m => m.Mobile == mobile);
                return users;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public byte? GetAccessable(string email)
        {
            try
            {
                var state = _context.Users.SingleOrDefault(m => m.Email == email).State;
                return (byte)state;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }


        }

        public void Remove(int id)
        {
            var item = Get(id);

            _context.Users.Remove(item);

            //item.State = (byte)UserState.Removed;
            //_context.Entry(item).State=EntityState.Modified;

        }

        public bool IsCorrectEmailPassword(string email, string password)
        {
            try
            {
                return _context.Users.Any(m => m.Email == email && m.Password == password);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public byte? GetStateAccount(string email, string password)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(m => m.Email == email && m.Password == password);
                return (byte)user.State;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public bool CanDelete(int id)
        {
            try
            {
                var user = Get(id);
                bool conflict = user != null; 
                return conflict;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public async Task<bool> CanDeleteAsync(int id)
        {
            try
            {
                var user = await GetAsync(id);
                bool conflict = true;// user.Foods.Any() || user.Invoices.Any();
                return conflict;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }

        public async Task<bool> ValidateLastChanged(string username, string lastChanged)
        {
            try
            {
                var user = await GetByEmailAsync(username);
                bool isValid = false;
                if (user != null)
                {
                    isValid = user.SecuritySpan.Equals(lastChanged);
                }
                else
                {
                    isValid = false;
                }

                return isValid;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }
        }
    }
}
