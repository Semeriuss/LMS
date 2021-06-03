﻿using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[Route("api/videos")]
public class VideosController : ControllerBase
{
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;
    private string pathForFiles = Environment.CurrentDirectory + "/" + "FileStorage";

    public VideosController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
    {
        _courseLibraryRepository = courseLibraryRepository ??
            throw new ArgumentNullException(nameof(courseLibraryRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }
    

    [HttpGet]
    public IActionResult Get(string filename)
    {
        string path = Path.Combine(pathForFiles, filename);
        return PhysicalFile(path, "application/octet-stream", enableRangeProcessing: true);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> PostResource(Guid contentId, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest("Make sure you have the file named file in form");
        }
        Resource resource = new Resource
        {
            FileName = file.FileName,
            FilePath = Path.Combine(pathForFiles, Path.GetRandomFileName()),
            ContentId = contentId,
            Id = new Guid()
        };

        await Task.Run(() => _courseLibraryRepository.AddResources(resource)); 

        using (var stream = System.IO.File.Create(resource.FilePath))
        {
            file.CopyTo(stream);
        }
        ResourceDto resourceToReturn = _mapper.Map<ResourceDto>(resource);

        return Ok(resourceToReturn);
    }


    [HttpGet]
    [Route("Download")]
    public async Task<IActionResult> Download()
    {
        string path = @"C:\Users\semer\Downloads\Video\1.mp4";
        var memory = new MemoryStream();
        using (FileStream stream = new FileStream(@"C:\Users\semer\Downloads\Video\1.mp4", FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, "application/octet-stream", Path.GetFileName(path), true); //enableRangeProcessing = true

    }


}