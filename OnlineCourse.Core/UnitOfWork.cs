using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineCourse.Core.Repositories.Interfaces;
using OnlineCourse.Entity;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Repositories;

namespace OnlineCourse.Core
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _context;
        private readonly HistoryService _historyService;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
            Users = new UserRepository(_context, _historyService);
            //Histories = new HistoryRepository(_context);
        }
        

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                _historyService.LogError(e, HistoryErrorType.Core);
                return -1;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
