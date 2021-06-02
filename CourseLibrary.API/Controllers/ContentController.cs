using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses/{courseId}/content")]

    public class ContentController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public ContentController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? 
                                       throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]   
        public ActionResult<IEnumerable<ContentDto>> GetContentsForAuthorAndCourse(Guid authorId, Guid courseId)
        {
            //if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            //{
            //    return NotFound();
            //}

            var contentsFromRepo = _courseLibraryRepository.GetContents(authorId, courseId);

            return Ok(_mapper.Map<IEnumerable<ContentDto>>(contentsFromRepo));
        }

        [HttpGet("{contentId}", Name = "GetContentForAuthorAndCourse")]
        public ActionResult<IEnumerable<ContentDto>> GetContentForAuthorAndCourse(Guid authorId, Guid courseId, Guid contentId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _courseLibraryRepository.GetContent(authorId, courseId, contentId);
            if (contentFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContentDto>(contentFromRepo));
        }

        [HttpPost]
        public ActionResult<ContentDto> CreateContent(Guid authorId, Guid courseId, ContentForCreationDto content)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentEntity = _mapper.Map<Entities.Content>(content);
            _courseLibraryRepository.AddContent(authorId, courseId, contentEntity);
            _courseLibraryRepository.Save();

            var contentToReturn = _mapper.Map<ContentDto>(contentEntity);

            return CreatedAtRoute("GetContentForAuthorAndCourse",
                new { authorId = authorId, courseId = courseId, contentId = contentToReturn.Id }, contentToReturn);
        }

        [HttpPut("{contentId}")]
        public IActionResult UpdateContent(Guid authorId, Guid courseId, Guid contentId, ContentForUpdateDto content)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _courseLibraryRepository.GetContent(authorId, courseId, contentId);

            if (contentFromRepo == null)
            {
                var contentToReturn = _mapper.Map<Entities.Content>(content);
                contentToReturn.Id = contentId;

                _courseLibraryRepository.AddContent(authorId, courseId, contentToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetContentForAuthorAndCourse",
                    new { authorId = authorId, courseId = courseId, contentId = contentToReturn.Id }, contentToReturn);
            }

            //map the entity to a CourseForUpdateDto
            //apply the updated field values to that dto
            //map the CourseForUpdateDto back to an entity
            _mapper.Map(content, contentFromRepo);

            _courseLibraryRepository.UpdateContent(contentFromRepo);

            _courseLibraryRepository.Save();
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public ActionResult DeleteContentForCourse(Guid authorId, Guid courseId, Guid contentId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId) || !_courseLibraryRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _courseLibraryRepository.GetContent(authorId, courseId, contentId);

            if (contentFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteContent(contentFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
    }
}
