using AutoMapper;
using DotNetCore.CS.API.CreatingAPI.Models;
using DotNetCore.CS.API.CreatingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetCore.CS.API.CreatingAPI.Controllers
{
  [ApiController]
  [Route("api/authors/{authorId}/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;

    public CoursesController(ICourseLibraryRepository courselibraryRepository, IMapper mapper)
    {
      _courseLibraryRepository = courselibraryRepository ??
        throw new ArgumentNullException(nameof(courselibraryRepository));

      _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
    {
      if (!_courseLibraryRepository.AuthorExists(authorId))
      {
        return NotFound();
      }

      var coursesForAuthorfromRepo = _courseLibraryRepository.GetCourses(authorId);
      return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorfromRepo));
    }

    [HttpGet("{courseId}")]
    public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
    {
      if (!_courseLibraryRepository.AuthorExists(authorId))
      {
        return NotFound();
      }

      var courseForAuthorfromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

      if (courseForAuthorfromRepo == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<CourseDto>(courseForAuthorfromRepo));
    }



  }
}
