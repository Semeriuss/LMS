using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.API.Services.ContentService;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/category/{categoryId}/courses/{courseId}/content")]

    public class ContentController : ControllerBase
    {
        private readonly IContentRepository _contentRepository;
        private readonly IMapper _mapper;

        public ContentController(IContentRepository contentRepository, IMapper mapper)
        {
            _contentRepository = contentRepository ?? 
                                       throw new ArgumentNullException(nameof(contentRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]   
        public ActionResult<IEnumerable<ContentDto>> GetContentsForCategoryAndCourse(Guid categoryId, Guid courseId)
        {
            if (!_contentRepository.CategoryExists(categoryId) || !_contentRepository.CourseExists(courseId))
            {
               return NotFound();
            }

            var contentsFromRepo = _contentRepository.GetContents(categoryId, courseId);

            return Ok(_mapper.Map<IEnumerable<ContentDto>>(contentsFromRepo));
        }

        [HttpGet("{contentId}", Name = "GetContentForCategoryAndCourse")]
        public ActionResult<IEnumerable<ContentDto>> GetContentForCategoryAndCourse(Guid categoryId, Guid courseId, Guid contentId)
        {
            if (!_contentRepository.CategoryExists(categoryId) || !_contentRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _contentRepository.GetContent(categoryId, courseId, contentId);
            if (contentFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ContentDto>(contentFromRepo));
        }

        [HttpPost]
        public ActionResult<ContentDto> CreateContent(Guid categoryId, Guid courseId, ContentForCreationDto content)
        {
            if (!_contentRepository.CategoryExists(categoryId) || !_contentRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentEntity = _mapper.Map<Entities.Content>(content);
            _contentRepository.AddContent(categoryId, courseId, contentEntity);
            _contentRepository.Save();

            var contentToReturn = _mapper.Map<ContentDto>(contentEntity);

            return CreatedAtRoute("GetContentForCategoryAndCourse",
                new { categoryId = categoryId, courseId = courseId, contentId = contentToReturn.Id }, contentToReturn);
        }

        [HttpPut("{contentId}")]
        public IActionResult UpdateContent(Guid categoryId, Guid courseId, Guid contentId, ContentForUpdateDto content)
        {
            if (!_contentRepository.CategoryExists(categoryId) || !_contentRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _contentRepository.GetContent(categoryId, courseId, contentId);

            if (contentFromRepo == null)
            {
                var contentToReturn = _mapper.Map<Entities.Content>(content);
                contentToReturn.Id = contentId;

                _contentRepository.AddContent(categoryId, courseId, contentToReturn);
                _contentRepository.Save();

                return CreatedAtRoute("GetContentForCategoryAndCourse",
                    new { categoryId = categoryId, courseId = courseId, contentId = contentToReturn.Id }, contentToReturn);
            }

            _mapper.Map(content, contentFromRepo);

            _contentRepository.UpdateContent(contentFromRepo);

            _contentRepository.Save();
            return NoContent();
        }

        [HttpDelete("{contentId}")]
        public ActionResult DeleteContentForCourse(Guid categoryId, Guid courseId, Guid contentId)
        {
            if (!_contentRepository.CategoryExists(categoryId) || !_contentRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var contentFromRepo = _contentRepository.GetContent(categoryId, courseId, contentId);

            if (contentFromRepo == null)
            {
                return NotFound();
            }

            _contentRepository.DeleteContent(contentFromRepo);
            _contentRepository.Save();

            return NoContent();

        }
    }
}
