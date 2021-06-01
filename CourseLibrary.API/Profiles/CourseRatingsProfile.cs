using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class CourseRatingsProfile : Profile
    {
        public CourseRatingsProfile()
        {
            CreateMap<Entities.CourseRating, Models.CourseRatingDto>();
            CreateMap<Models.CourseRatingForManipulationDto, Entities.CourseRating>();
            CreateMap<Entities.CourseRating, Models.CourseForManipulationDto>();
        }
    }
}
