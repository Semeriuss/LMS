﻿using AutoMapper;
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

        [HttpGet(Name = "GetRatingOfUser")]
        public ActionResult<CourseRatingDto> GetRatingOfUser(Guid CourseId, Guid AuthorId)
        {
            if (!_courseLibraryRepository.AuthorExists(AuthorId) || !_courseLibraryRepository.CourseExists(CourseId))
            {
                return NotFound();
            }

            var ratingsForAuthorFromRepo = _courseLibraryRepository.GetRating(AuthorId, CourseId);
            return Ok(_mapper.Map<CourseRatingDto>(ratingsForAuthorFromRepo));
        }

        [HttpGet]
        public ActionResult<double> GetAverageRating(Guid CourseId)
        {
            if (!_courseLibraryRepository.CourseExists(CourseId))
            {
                return NotFound();
            }

            double averageRatingForCourseFromRepo = _courseLibraryRepository.GetRatings(CourseId);

            //if (averageRatingForCourseFromRepo == null)
            //{
            //    return NotFound();
            //}
            return Ok(averageRatingForCourseFromRepo);
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateRatingForAuthor(Guid authorId, Guid courseId, CourseRatingForManipulationDto courseRating)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingEntity = _mapper.Map<Entities.CourseRating>(courseRating);
            _courseLibraryRepository.AddRating(authorId, courseId, ratingEntity);
            _courseLibraryRepository.Save();

            var ratingsToReturn = _mapper.Map<CourseDto>(ratingEntity);
            return CreatedAtRoute("GetRatingOfUser",
                new { ratingsToReturn.Id, authorId, courseId }, ratingsToReturn);
        }

        [HttpPut]

        public IActionResult UpdateRatingForAuthor(Guid authorId, Guid courseId, CourseRatingForManipulationDto courseRating)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _courseLibraryRepository.GetRating(authorId, courseId);

            if (ratingForAuthorFromRepo == null)
            {
                var ratingToReturn = _mapper.Map<Entities.CourseRating>(courseRating);
                ratingToReturn.CourseId = courseId;

                _courseLibraryRepository.AddRating(authorId, courseId, ratingToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetCourseForAuthor",
                    new { ratingToReturn.Id, authorId, courseId = ratingToReturn.Id }, ratingToReturn);
            }

            //map the entity to a CourseForUpdateDto
            //apply the updated field values to that dto
            //map the CourseForUpdateDto back to an entity
            _mapper.Map(courseRating, ratingForAuthorFromRepo);

            _courseLibraryRepository.UpdateRating(ratingForAuthorFromRepo);

            _courseLibraryRepository.Save();
            return NoContent();
        }

        [HttpDelete]

        public ActionResult DeleteRatingForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var ratingForAuthorFromRepo = _courseLibraryRepository.GetRating(authorId, courseId);

            if (ratingForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteRating(ratingForAuthorFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
        //public override ActionResult ValidationProblem(
        //    [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        //{
        //    var options = HttpContext.RequestServices
        //        .GetRequiredService<IOptions<ApiBehaviorOptions>>();

        //    return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        //}
    }
}
