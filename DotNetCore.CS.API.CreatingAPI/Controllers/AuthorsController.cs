using AutoMapper;
using DotNetCore.CS.API.CreatingAPI.Models;
using DotNetCore.CS.API.CreatingAPI.ResourceParameters;
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

    [HttpGet("{id}", Name = "GetAuthor")]
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

      return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
    }

    //[HttpGet()]
    //[HttpHead]
    //// public ActionResult<IEnumerable<AuthorDto>> GetAuthors(string mainCategory) //This is fine but better readability we can you use
    ////public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] string mainCategory) //using attribute [fromquery] helps increasing readability
    ////ideally we need to use the [FromQuery(Name="<key for query string>")] using the name property helps you hide the query string and implementation detail
    //public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery(Name = "mc")] string mainCategory, [FromQuery] string search)
    //{
    //  var authorsFromRepo = _courseLibraryRepository.GetAuthors(mainCategory, search);


    //  return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
    //}

    [HttpGet()]
    [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParms)
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParms);


      return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
    }

    [HttpPost()]
    public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
    {
      //this is not need since we are using APIController Attribute which already does this check and retrun
      //bad request in case of base values and bad parameters
      //if (author == null)
      //{
      //  return BadRequest();
      //}

      var authorEntity = _mapper.Map<Entities.Author>(author);
      _courseLibraryRepository.AddAuthor(authorEntity);
      _courseLibraryRepository.Save();

      var authorToReturn= _mapper.Map<AuthorDto>(authorEntity);
      return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id }, authorToReturn);
    }
  }
}
