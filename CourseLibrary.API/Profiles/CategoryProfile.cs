using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Entities.Category, Models.CategoryDto>();
            CreateMap<Models.CategoryForCreationDto, Entities.Category>();
            CreateMap<Entities.Category, Models.CategoryDto>();
            CreateMap<Models.CategoryForUpdateDto, Entities.Category>();
        }
    }
    
}
