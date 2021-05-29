using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
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
    [Route("api/coursecatagory")]
    public class CatagoryController:ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CatagoryController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CatagoryDto>> GetCatagories(Guid catagoryId)
        {
            if (!_courseLibraryRepository.CatagoryExists(catagoryId))
            {
                return NotFound();
            }

            var catagoryFromRepo = _courseLibraryRepository.GetCatagory(catagoryId);
            return Ok(_mapper.Map<IEnumerable<CatagoryDto>>(catagoryFromRepo));
        }
        [HttpPost]
        public ActionResult CreateNewCatagory(Guid catagoryId, CatagoryForCreationDto catagory)
        {
            if (!_courseLibraryRepository.CatagoryExists(catagoryId))
            {
                return NotFound();
            }

            var catagoryEntity = _mapper.Map<Entities.Course>( );
            _courseLibraryRepository.AddCatagory(catagoryId, catagoryEntity);
            _courseLibraryRepository.Save();

            var catagoryToReturn = _mapper.Map<CatagoryDto>(catagoryEntity);
            return CreatedAtRoute("GetCatagoryForAuthorAndCourse",
                new { catagoryId = catagoryId, catagoryId = catagoryToReturn.Id }, catagoryToReturn);
        }
        [HttpDelete]
        public ActionResult DeleteCatagory()
        {

        }
    }
    }
