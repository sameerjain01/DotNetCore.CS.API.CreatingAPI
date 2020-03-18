using AutoMapper;
using System;

namespace DotNetCore.CS.API.CreatingAPI.Profiles
{
  public class CoursesProfile :  Profile
  {
    public CoursesProfile()
    {
      CreateMap<Entities.Course, Models.CourseDto>();
      CreateMap<Models.CourseForCreationDto, Entities.Course>();
    }
  }
}
