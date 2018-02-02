using Microsoft.EntityFrameworkCore;
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
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HistoryService _historyService;

        public GalleryRepository(ApplicationDbContext context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
        }

        public IEnumerable<Gallery> GetAll()
        {
            try
            {
                return _context.Galleries.AsQueryable();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

        public async Task<IEnumerable<Gallery>> GetAllAsync()
        {
            try
            {
                return _context.Galleries.AsQueryable();
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Core);
                throw;
            }

        }

        public IQueryable<Gallery> GetAll(int userid)
        {
            try
            {
               return _context.Galleries.Where(m => m.PublicId==userid && m.Kind==(byte)GalleryKind.UserProfile);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }


        public Gallery Get(int id)
        {
            try
            {
                return _context.Galleries.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }
        public async Task<Gallery> GetAsync(int id)
        {
            try
            {
                return await _context.Galleries.SingleOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

        public IEnumerable<Gallery> GetGallery(int publicId, byte kind)
        {
            try
            {
                return _context.Galleries.Where(m => m.PublicId == publicId && m.Kind == kind).ToList();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

        public async Task<IEnumerable<Gallery>> GetGalleryAsync(int publicId, byte kind)
        {
            try
            {
                return await _context.Galleries.Where(m => m.PublicId == publicId && m.Kind == kind).ToListAsync();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

        public async Task<Gallery> GetUserProfileAsync(int publicId)
        {
            try
            {
                return await _context.Galleries.SingleOrDefaultAsync(m => m.PublicId == publicId && m.Kind == (byte)GalleryKind.UserProfile);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

        }

       

        public void Add(Gallery gallery)
        {
            try
            {
                _context.Galleries.Add(gallery);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);

                throw;
            }

        }

        public void Update(Gallery user)
        {

            try
            {
                _context.Galleries.Update(user);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }

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

        public void Remove(int id)
        {
            try
            {
                var item = Get(id);
                _context.Entry(item).State = EntityState.Deleted;
                _context.Galleries.Remove(item);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Core);
                throw;
            }



        }
    }
}
