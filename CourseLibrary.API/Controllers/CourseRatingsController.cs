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

namespace CourseLibrary.API.Controllers
{
    [Route("api/authors/{authorId}/courses/{courseId}/rating")]
    [ApiController]
    public class CourseRatingsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CourseRatingsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{ratingId}", Name = "GetRatingOfUser")]
        public ActionResult GetRatingOfUser(Guid CourseId, Guid AuthorId, Guid ratingId)
        {
            if (!_courseLibraryRepository.AuthorExists(AuthorId) || !_courseLibraryRepository.CourseExists(CourseId))
            {
                return NotFound();
            }

            var ratingsForAuthorFromRepo = _courseLibraryRepository.GetRating(AuthorId, CourseId, ratingId);
            return Ok(_mapper.Map<CourseRatingDto>(ratingsForAuthorFromRepo));
        }

        [HttpGet(Name = "GetAverageRating")]
        public ActionResult GetAverageRating(Guid CourseId)
        {
            if (!_courseLibraryRepository.CourseExists(CourseId) || !_courseLibraryRepository.CourseRatingExists(CourseId))
            {
                return NotFound();
            }

            double averageRatingForCourseFromRepo = _courseLibraryRepository.GetRatings(CourseId);
            return Ok(averageRatingForCourseFromRepo);
        }

        [HttpPost]
        public ActionResult<CourseRatingDto> CreateRatingForAuthor(Guid authorId, Guid courseId, [FromBody] CourseRatingForManipulationDto courseRating)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingEntity = _mapper.Map<Entities.CourseRating>(courseRating);
            _courseLibraryRepository.AddRating(authorId, courseId, ratingEntity);
            _courseLibraryRepository.Save();

            var ratingToReturn = _mapper.Map<CourseRatingDto>(ratingEntity);
            return CreatedAtRoute("GetRatingOfUser",
                new { courseId, authorId, ratingId = ratingToReturn.Id }, ratingToReturn);
        }

        [HttpPut("{ratingId}")]
        public IActionResult UpdateRatingForAuthor(Guid authorId, Guid courseId, Guid ratingId, CourseRatingForManipulationDto courseRating)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _courseLibraryRepository.GetRating(authorId, courseId, ratingId);

            if (ratingForAuthorFromRepo == null)
            {
                var ratingToReturn = _mapper.Map<Entities.CourseRating>(courseRating);
                ratingToReturn.Id = ratingId;

                _courseLibraryRepository.AddRating(authorId, courseId, ratingToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetRatingForUser",
                    new { ratingToReturn.Id, authorId, courseId = ratingToReturn.Id }, ratingToReturn);
            }

            _mapper.Map(courseRating, ratingForAuthorFromRepo);

            _courseLibraryRepository.UpdateRating(ratingForAuthorFromRepo);

            _courseLibraryRepository.Save();
            return NoContent();
        }

        [HttpDelete("{ratingId}")]

        public ActionResult DeleteRatingForAuthor(Guid authorId, Guid courseId, Guid ratingId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _courseLibraryRepository.GetRating(authorId, courseId, ratingId);

            if (ratingForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteRating(ratingForAuthorFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
    }
}
