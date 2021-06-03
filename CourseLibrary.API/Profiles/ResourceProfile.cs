using System;
using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<Entities.Resource, Models.ResourceDto>();
        }
    }
}
