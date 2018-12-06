using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils
{

    public class Uploder
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly HistoryService _historyService;

        public Uploder(IHostingEnvironment hostingEnvironment, HistoryService historyService)
        {
            _hostingEnvironment = hostingEnvironment;
            _historyService = historyService;
        }

        public async Task<string> UploadGalleryAsync(string fileName, IFormFile file)
        {
            try
            {
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath,
                    Path.Combine("uploads", "galleries"));

                var filenameandpath = Path.Combine(uploadsRootFolder, fileName) + Path.GetExtension(file.FileName);

                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filenameandpath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                return filenameandpath;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Middle);
                throw;
            }

        }

        public string UploadGallery(string fileName, IFormFile file)
        {
            try
            {
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath,
                    Path.Combine("uploads", "galleries"));

                var filenameandpath = Path.Combine(uploadsRootFolder, fileName) + Path.GetExtension(file.FileName);

                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filenameandpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return filenameandpath;
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.Middle);
                throw;
            }

        }
    }
}
