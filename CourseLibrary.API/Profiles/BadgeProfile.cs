using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class BadgeProfile : Profile
    {
        public BadgeProfile() {
            CreateMap<Entities.Badge, Models.BadgeDto>();
            CreateMap<Models.BadgeForUpdateDto, Entities.Badge>();
            CreateMap<Entities.Course, Models.BadgeForUpdateDto>();
        }
    }
    }
}
