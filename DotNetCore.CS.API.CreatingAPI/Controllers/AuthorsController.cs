using AutoMapper;
using DotNetCore.CS.API.CreatingAPI.Models;
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
    private readonly IMapper _mapper;

    public AuthorsController(ICourseLibraryRepository courselibraryRepository, IMapper mapper)
    {
      _courseLibraryRepository = courselibraryRepository ??
        throw new ArgumentNullException(nameof(courselibraryRepository));
     
      _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    }

    //[HttpGet("api/authors/test")]
    //public IActionResult TestGetAuthors()
    //{


    //  return new OkResult();
    //}

    [HttpGet("{id}")]
    public ActionResult<AuthorDto> GetAuthor(Guid id)
    {
      
      //NOT Great code since two call
      //if (!_courseLibraryRepository.AuthorExists(id))
      //{
      //  return NotFound();
      //}

      var authorFromRepo = _courseLibraryRepository.GetAuthor(id);
      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return  Ok(_mapper.Map<AuthorDto>(authorFromRepo));
    }

    [HttpGet()]
    [HttpHead]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors();


      return Ok(_mapper.Map< IEnumerable<AuthorDto>>(authorsFromRepo));
    }
  }
}
