using Microsoft.AspNetCore.Http;
using OnlineCourse.Panel.Utils.HtmlHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils.ViewModels.Areas.Admin
{

    public class CreateUserViewModel
    {

        public int Id { set; get; }

        [MaxLength(100)]
        [Display(Name = "UserName")]
        public string UserName { set; get; }

        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [MaxLength(20)]
        [Display(Name = "FullName")]
        public string FullName { set; get; }

        [MaxLength(200)]
        [Display(Name = "Password")]
        public string Pwd { get; set; }

        [MaxLength(50)]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [MaxLength(400)]
        [Display(Name = "Description")]
        public string Des { get; set; }


        [Display(Name = "AccessLevel")]
        public byte? AccessLevel { get; set; }


        [Display(Name = "State")]
        public byte? State { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "ExpireDate")]
        [DataType(DataType.Text)]
        public DateTime? ExpireDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "Addrress")]
        public string Addrress { set; get; }

        [MaxLength(30)]
        [Display(Name = "Position")]
        public string Position { set; get; }

        [MaxLength(30)]
        [Display(Name = "WorkDays")]
        public string WorkDays { set; get; }

        [MaxLength(50)]
        [Display(Name = "City")]
        public string City { set; get; }

        [MaxLength(50)]
        [Display(Name = "Country")]
        public string Country { set; get; }


        [Display(Name = "OnOff")]
        public byte? OnOff { set; get; }


        [Display(Name = "Image")]
        [UploadFileExtensions(".png,.jpg,.jpeg,.gif", ErrorMessage = "ChooseImageExtValidationMessage")]
        [Required(ErrorMessage = "PublicRequireValidation")]
        [DataType(DataType.Upload)]
        public IFormFile Image { set; get; }



    }
}
