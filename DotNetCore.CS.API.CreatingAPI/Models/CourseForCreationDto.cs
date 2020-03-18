using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.CS.API.CreatingAPI.Models
{
  public class CourseForCreationDto
  {
      public string Title { get; set; }
    public string Description { get; set; }
    //public Guid AuthorId { get; set; } //NOt adding to body as this is avaible in URL

  }
}
