using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/badge")]
    public class BadgeController : Controller
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public BadgeController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{UserId}")]
        [HttpHead]
        public IActionResult GetBadgeForUser(Guid UserId)
        {

            var badgeFromRepo = _courseLibraryRepository.GetBadge(UserId);

            if (badgeFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BadgeDto>(badgeFromRepo));
        }
        /*
        [HttpGet("{UserId}/videoBadge")]
        public IActionResult GetVideoBadgeForUser(Guid UserId)
        {

            var vbadgeFromRepo = _courseLibraryRepository.GetVideoBadge(UserId);

            if (vbadgeFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BadgeDto>(vbadgeFromRepo));
        }
        [HttpGet("{UserId}/noteBadge")]
        public IActionResult GetNoteBadgeForUser(Guid UserId)
        {

            var badgeFromRepo = _courseLibraryRepository.GetNoteBadge(UserId);

            if (badgeFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BadgeDto>(badgeFromRepo));
        }
        [HttpGet("{UserId}/quizBadge")]
        public IActionResult GetQuizBadgeForUser(Guid UserId)
        {

            var badgeFromRepo = _courseLibraryRepository.GetQuizBadge(UserId);

            if (badgeFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BadgeDto>(badgeFromRepo));
            
        }*/
        [HttpPut("{UserId}")]
        public IActionResult UpdateBadgeNumber(Guid BadgeId, Guid UserId, BadgeForUpdateDto badge)
        {
            if (!_courseLibraryRepository.UserExists(BadgeId))
            {
                return NotFound();
            }
        
         

            var badgeFromRepo = _courseLibraryRepository.GetBadge(UserId);

            if (badgeFromRepo == null)
            {
                var badgeToReturn = _mapper.Map<Entities.Badge>(badge);
                badgeToReturn.BadgeId = BadgeId;

                _courseLibraryRepository.GetBadgeNum(BadgeId, badgeToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetBadgeForUser",
                    new { BadgeId = badgeToReturn.BadgeId }, badgeToReturn);
            }

            //map the entity to a CourseForUpdateDto
            //apply the updated field values to that dto
            //map the CourseForUpdateDto back to an entity
            _mapper.Map(badge, badgeFromRepo
                );

            _courseLibraryRepository.UpdateCourse(badgeFromRepo);

            _courseLibraryRepository.Save();
            return NoContent();
        }



    }
}
