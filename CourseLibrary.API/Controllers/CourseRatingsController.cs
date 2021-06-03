 using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services.RatingService;

namespace CourseLibrary.API.Controllers
{
    [Route("api/category/{categoryId}/courses/{courseId}/rating")]
    [ApiController]
    public class CourseRatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;
        public CourseRatingsController(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository ??
                throw new ArgumentNullException(nameof(ratingRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{ratingId}", Name = "GetRatingOfUser")]
        public ActionResult GetRatingOfUser(Guid CourseId, Guid categoryId, Guid ratingId)
        {
            if (!_ratingRepository.CategoryExists(categoryId) || !_ratingRepository.CourseExists(CourseId))
            {
                return NotFound();
            }

            var ratingsForAuthorFromRepo = _ratingRepository.GetRating(categoryId, CourseId, ratingId);
            return Ok(_mapper.Map<CourseRatingDto>(ratingsForAuthorFromRepo));
        }

        [HttpGet(Name = "GetAverageRating")]
        public ActionResult GetAverageRating(Guid CourseId)
        {
            if (!_ratingRepository.CourseExists(CourseId) || !_ratingRepository.CourseRatingExists(CourseId))
            {
                return NotFound();
            }

            double averageRatingForCourseFromRepo = _ratingRepository.GetRatings(CourseId);
            return Ok(averageRatingForCourseFromRepo);
        }

        [HttpPost]
        public ActionResult<CourseRatingDto> CreateRatingForAuthor(Guid categoryId, Guid courseId, [FromBody] CourseRatingForManipulationDto courseRating)
        {
            if (!_ratingRepository.CategoryExists(categoryId) || !_ratingRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingEntity = _mapper.Map<Entities.CourseRating>(courseRating);
            _ratingRepository.AddRating(categoryId, courseId, ratingEntity);
            _ratingRepository.Save();

            var ratingToReturn = _mapper.Map<CourseRatingDto>(ratingEntity);
            return CreatedAtRoute("GetRatingOfUser",
                new { courseId, categoryId, ratingId = ratingToReturn.Id }, ratingToReturn);
        }

        [HttpPut("{ratingId}")]
        public IActionResult UpdateRatingForAuthor(Guid categoryId, Guid courseId, Guid ratingId, CourseRatingForManipulationDto courseRating)
        {
            if (!_ratingRepository.CategoryExists(categoryId) || !_ratingRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _ratingRepository.GetRating(categoryId, courseId, ratingId);

            if (ratingForAuthorFromRepo == null)
            {
                var ratingToReturn = _mapper.Map<Entities.CourseRating>(courseRating);
                ratingToReturn.Id = ratingId;

                _ratingRepository.AddRating(categoryId, courseId, ratingToReturn);
                _ratingRepository.Save();

                return CreatedAtRoute("GetRatingForUser",
                    new { ratingToReturn.Id, categoryId, courseId = ratingToReturn.Id }, ratingToReturn);
            }

            _mapper.Map(courseRating, ratingForAuthorFromRepo);

            _ratingRepository.UpdateRating(ratingForAuthorFromRepo);

            _ratingRepository.Save();
            return NoContent();
        }

        [HttpDelete("{ratingId}")]

        public ActionResult DeleteRatingForAuthor(Guid categoryId, Guid courseId, Guid ratingId)
        {
            if (!_ratingRepository.CategoryExists(categoryId) || !_ratingRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _ratingRepository.GetRating(categoryId, courseId, ratingId);

            if (ratingForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _ratingRepository.DeleteRating(ratingForAuthorFromRepo);
            _ratingRepository.Save();

            return NoContent();

        }
    }
}
