using DotNetCore.CS.API.CreatingAPI.Entities;
using DotNetCore.CS.API.CreatingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetCore.CS.API.CreatingAPI.Controllers
{
  [ApiController]
  [Route("api/authors")]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    public AuthorsController(ICourseLibraryRepository courselibraryRepository)
    {
      _courseLibraryRepository = courselibraryRepository ?? throw new ArgumentNullException(nameof(courselibraryRepository));
    }

    //[HttpGet("api/authors/test")]
    //public IActionResult TestGetAuthors()
    //{


    //  return new OkResult();
    //}

    [HttpGet("{id}")]
    public IActionResult GetAuthor(Guid id)
    {
      
      //NOT Great code since two call
      //if (!_courseLibraryRepository.AuthorExists(id))
      //{
      //  return NotFound();
      //}

      var authors = _courseLibraryRepository.GetAuthor(id);
      if (authors == null)
      {
        return NotFound();
      }

      return  Ok(authors);
    }

    [HttpGet()]
    public IActionResult GetAuthors()
    {
      var authors = _courseLibraryRepository.GetAuthors();


      return Ok(authors);
    }
  }
}
