using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services.CategoryService;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            var categoriesFromRepo = _categoryRepository.GetCategories();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categoriesFromRepo));
        }

        [HttpGet("{categoryId}", Name = "GetCategory")]
        public IActionResult GetCategory(Guid categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }  

            return Ok(_mapper.Map<CategoryDto>(categoryFromRepo));
        }

        [HttpPost]
        public ActionResult<CategoryDto> CreateCategory(CategoryForCreationDto category)
        {   
            var categoryEntity = _mapper.Map<Entities.Category>(category);
            _categoryRepository.AddCategory(categoryEntity);
            _categoryRepository.Save();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("GetCategory",
                new { CategoryId = categoryToReturn.Id }, categoryToReturn);
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(Guid CategoryId, CategoryForUpdateDto category)
        {
            if (!_categoryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }

            var categoryFromRepo = _categoryRepository.GetCategory(CategoryId);

            if (categoryFromRepo == null)
            {
                var categoryToReturn = _mapper.Map<Entities.Category>(category);
                categoryToReturn.Id = CategoryId;

                _categoryRepository.AddCategory(categoryToReturn);
                _categoryRepository.Save();

                return CreatedAtRoute("GetCategory",
                    new { CategoryId = categoryToReturn.Id }, categoryToReturn);
            }

            _mapper.Map(category, categoryFromRepo);

            _categoryRepository.UpdateCategory(categoryFromRepo);

            _categoryRepository.Save();
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(Guid categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryFromRepo);
            _categoryRepository.Save();

            return NoContent();

        }
        
    }
}

