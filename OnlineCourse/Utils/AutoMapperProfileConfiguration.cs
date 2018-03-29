using AutoMapper;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils.ViewModels.Areas.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourse.Panel.Utils
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {
            CreateMap<CreateUserViewModel, User>();
            CreateMap<User, CreateUserViewModel>();

            CreateMap<SectionCreateViewModel, Section>();
            CreateMap<Section, SectionCreateViewModel>();

            CreateMap<SectionEditViewModel, Section>();
            CreateMap<Section, SectionEditViewModel>();

        }
    }
}
