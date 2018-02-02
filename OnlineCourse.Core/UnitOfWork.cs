using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineCourse.Core.Repositories.Interfaces;
using OnlineCourse.Entity;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Repositories;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Core
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _context;
        private readonly HistoryService _historyService;
        public IUserRepository Users { get; private set; }
        //public GenericRepository<User> userRepository { get; private set; }
        //public GenericRepository<Gallery> GalleryRepository { get; private set; }

        public IGalleryRepository Galleries { get; }

        public UnitOfWork(ApplicationDbContext context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
            Users = new UserRepository(_context, _historyService);
            Galleries = new GalleryRepository(_context, historyService);
            //Histories = new HistoryRepository(_context);
        }
               
        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                _historyService.LogError(e, HistoryErrorType.Core);
                return -1;
            }
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

        //private bool disposed = false;

        //public void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    disposed = true;
        //}
    }
}
