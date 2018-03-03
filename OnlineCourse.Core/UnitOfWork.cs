using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineCourse.Core.Repositories.Interfaces;
using OnlineCourse.Entity;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.Repositories;
using OnlineCourse.Entity.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourse.Core
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _context;
        private readonly HistoryService _historyService;
        public IUserRepository Users { get; private set; }
        //public GenericRepository<User> userRepository { get; private set; }
        //public GenericRepository<Gallery> GalleryRepository { get; private set; }
        public GenericRepository<History> HistoryRepository { get; private set; }

        public IGalleryRepository Galleries { get; }

        public UnitOfWork(ApplicationDbContext context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
            Users = new UserRepository(_context, _historyService);
            Galleries = new GalleryRepository(_context, historyService);
            HistoryRepository = new GenericRepository<History>(_context);
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

        public void UnTracking()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
                    .ToList();
                    foreach (var entity in changedEntriesCopy)
                    {
                        _context.Entry(entity.Entity).State = EntityState.Detached;
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
