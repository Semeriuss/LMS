using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/coursecatagory")]
    public class CategoryController:ControllerBase
    {   
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories(Guid categoryId, Array Categories)
        {   
            if (!_courseLibraryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var catagoryFromRepo = _courseLibraryRepository.GetCategory(categoryId);
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(catagoryFromRepo));
        }

        [HttpPost]
        public ActionResult<CategoryDto> CreateNewCategory(Guid categoryId, CategoryForCreationDto category)
        {   
            if (!_courseLibraryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryEntity = _mapper.Map<Entities.Category>( );
            _courseLibraryRepository.AddCategory(categoryId, categoryEntity);
            _courseLibraryRepository.Save();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("GetCatagoryForCourse",
                new { categoryId = categoryToReturn.Id }, categoryToReturn);
        }
        [HttpPut("{categoryId}")]

        public IActionResult UpdateCategoryForCourse(Guid categoryId, CategoryForUpdateDto category)
        {
            if (!_courseLibraryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryForCourseFromRepo = _courseLibraryRepository.GetCategory(categoryId);

            if (categoryForCourseFromRepo == null)
            {
                var categoryToReturn = _mapper.Map<Entities.Category>(category);
                categoryToReturn.Id = categoryId;

                _courseLibraryRepository.AddCategory(categoryId, categoryToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetCategoryForCourse",
                    new { categoryId = categoryToReturn.Id }, categoryToReturn);
            }

            //map the entity to a CourseForUpdateDto
            //apply the updated field values to that dto
            //map the CourseForUpdateDto back to an entity
            _mapper.Map(category, categoryForCourseFromRepo);

            _courseLibraryRepository.UpdateCategory(categoryForCourseFromRepo);

            _courseLibraryRepository.Save();
            return NoContent();
        }
        [HttpDelete]
        public ActionResult DeleteACategory(Guid categoryId)
        {
            if (!_courseLibraryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryForCourseFromRepo = _courseLibraryRepository.GetCategory(categoryId);

            if (categoryForCourseFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteCategory(categoryForCourseFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
        
    }
}

