using OnlineCourse.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Core.Repositories.Interfaces
{
    public interface IGalleryRepository
    {
        IEnumerable<Gallery> GetAll();
        Task<IEnumerable<Gallery>> GetAllAsync();
        IQueryable<Gallery> GetAll(int chefId);
        Gallery Get(int id);
        Task<IEnumerable<Gallery>> GetGalleryAsync(int publicId, byte kind);
        IEnumerable<Gallery> GetGallery(int publicId, byte kind);
        void Add(Gallery gallery);
        void Update(Gallery gallery);
        void Remove(int id);
        bool IsExist(int id);
        Task<Gallery> GetAsync(int idValue);        
        Task<Gallery> GetUserProfileAsync(int publicId);
    }
}
