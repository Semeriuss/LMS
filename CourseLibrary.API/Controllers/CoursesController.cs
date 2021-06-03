using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services.CourseService;
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
    [ApiController]
    [Route("api/category/{categoryId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForCategory(Guid categoryId)
        {
            if (!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var coursesForAuthorFromRepo = _courseRepository.GetCourses(categoryId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
        }

        [HttpGet("{courseId}", Name = "GetCourseForCategory")]
        public ActionResult<IEnumerable<CourseDto>> GetCourseForAuthor(Guid categoryId, Guid CourseId)
        {
            if (!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseRepository.GetCourse(categoryId, CourseId);

            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid categoryId, CourseForCreationDto course)
        {
            if (!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Entities.Course>(course);
            _courseRepository.AddCourse(categoryId, courseEntity);
            _courseRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForCategory",
                new { categoryId = categoryId, courseId = courseToReturn.Id }, courseToReturn);
        }

        [HttpPut("{courseId}")]

        public IActionResult UpdateCourseForAuthor(Guid categoryId, Guid courseId, CourseForUpdateDto course)
        {
            if (!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseRepository.GetCourse(categoryId, courseId);

            if (courseForAuthorFromRepo == null)
            {
                var courseToReturn = _mapper.Map<Entities.Course>(course);
                courseToReturn.Id = courseId;

                _courseRepository.AddCourse(categoryId, courseToReturn);
                _courseRepository.Save();

                return CreatedAtRoute("GetCourseForCategory",
                    new { categoryId, courseId = courseToReturn.Id}, courseToReturn);
            }

            //map the entity to a CourseForUpdateDto
            //apply the updated field values to that dto
            //map the CourseForUpdateDto back to an entity
            _mapper.Map(course, courseForAuthorFromRepo);

            _courseRepository.UpdateCourse(courseForAuthorFromRepo);

            _courseRepository.Save();
            return NoContent();
        }

        [HttpPatch("{courseId}")]
        
        public ActionResult PartiallyUpdateCourseForAuthor(Guid categoryId,
            Guid courseId, 
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if(!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseRepository.GetCourse(categoryId, courseId);

            if(courseForAuthorFromRepo == null)
            {
                var courseDto = new CourseForUpdateDto();
                patchDocument.ApplyTo(courseDto, ModelState);

                if(!TryValidateModel(courseDto))
                {
                    return ValidationProblem(ModelState);
                }
                var courseToAdd = _mapper.Map<Entities.Course>(courseDto);
                courseToAdd.Id = courseId;

                _courseRepository.AddCourse(categoryId, courseToAdd);
                _courseRepository.Save();

                var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);

                return CreatedAtRoute("GetCourseForCategory",
                    new { categoryId, courseId = courseToReturn.Id, },
                    courseToReturn);
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseForAuthorFromRepo);
            //Add Validation
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if(!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(courseToPatch, courseForAuthorFromRepo);

            _courseRepository.UpdateCourse(courseForAuthorFromRepo);

            _courseRepository.Save();

            return NoContent();
        }

        [HttpDelete("{courseId}")]

        public ActionResult DeleteCourseForAuthor(Guid categoryId, Guid courseId)
        {
            if (!_courseRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseRepository.GetCourse(categoryId, courseId);

            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _courseRepository.DeleteCourse(courseForAuthorFromRepo);
            _courseRepository.Save();

            return NoContent();

        }
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
