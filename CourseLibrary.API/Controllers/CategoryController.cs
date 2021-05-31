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
    [Route("api/category")]
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
        [HttpGet("{CourseId}")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategoryForCourse(Guid CourseId)
        {   
            if (!_courseLibraryRepository.CourseExists(CourseId))
            {
                return NotFound();
            }

            var catagoryFromRepo = _courseLibraryRepository.GetCategory(CourseId);
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(catagoryFromRepo));
        }
        //[HttpGet("{CategoryId}", Name = "GetCategoryForCourse")]
        //public ActionResult<IEnumerable<CategoryDto>> GetCategoryForCourse(Guid CategoryId)
        //{
        //    if (!_courseLibraryRepository.CategoryExists(CategoryId))
        //    {
        //        return NotFound();
        //    }

        //    var categoryFromRepo = _courseLibraryRepository.GetCategory(CategoryId);
        //    if (categoryFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<CategoryDto>(categoryFromRepo));
        //}

        [HttpPost]
        public ActionResult<CategoryDto> CreateNewCategory(Guid CategoryId, CategoryForCreationDto category)
        {   
            if (!_courseLibraryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }

            var categoryEntity = _mapper.Map<Entities.Category>(category);
            _courseLibraryRepository.AddCategory(categoryEntity);
            _courseLibraryRepository.Save();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("GetCatagoryForCourse",
                new { CategoryId = categoryToReturn.CategoryId }, categoryToReturn);
        }
        [HttpPut("{CategoryId}")]

        public IActionResult UpdateCategoryForCourse(Guid CategoryId, CategoryForUpdateDto category)
        {
            if (!_courseLibraryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }

            var categoryForCourseFromRepo = _courseLibraryRepository.GetCategory(CategoryId);

            if (categoryForCourseFromRepo == null)
            {
                var categoryToReturn = _mapper.Map<Entities.Category>(category);
                categoryToReturn.categoryId = CategoryId;

                _courseLibraryRepository.AddCategory(categoryToReturn);
                _courseLibraryRepository.Save();

                return CreatedAtRoute("GetCategoryForCourse",
                    new { CategoryId = categoryToReturn.categoryId }, categoryToReturn);
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
        public ActionResult DeleteACategory(Guid CategoryId)
        {
            if (!_courseLibraryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }

            var categoryForCourseFromRepo = _courseLibraryRepository.GetCategory(CategoryId);

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

