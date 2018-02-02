using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.HtmlHelper
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UploadFileExtensionsAttribute : ValidationAttribute
    {
        private readonly IList<string> _allowedExtensions;
        public UploadFileExtensionsAttribute(string fileExtensions)
        {
            _allowedExtensions = fileExtensions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                return isValidFile(file);
            }

            //var files = value as IList<IFormFile>;
            //if (files == null)
            //{
            //    return false;
            //}

            //foreach (var postedFile in files)
            //{
            //    if (!isValidFile(postedFile)) return false;
            //}

            return true;
        }

        private bool isValidFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return true;
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var resss = !string.IsNullOrWhiteSpace(fileExtension) &&
                       _allowedExtensions.Any(ext => fileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase));
            return resss;
        }
    }
}
