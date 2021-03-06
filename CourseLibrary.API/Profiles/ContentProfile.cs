using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<Entities.Content, Models.ContentDto>();
            CreateMap<Models.ContentForCreationDto, Entities.Content>();
            CreateMap<Entities.Content, Models.CourseDto>();
            CreateMap<Models.ContentForUpdateDto, Entities.Content>();
        }
    }
}
