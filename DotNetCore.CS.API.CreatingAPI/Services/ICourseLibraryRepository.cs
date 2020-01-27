using DotNetCore.CS.API.CreatingAPI.Entities;
using DotNetCore.CS.API.CreatingAPI.ResourceParameters;
using System;
using System.Collections.Generic;


namespace DotNetCore.CS.API.CreatingAPI.Services
{
  public interface ICourseLibraryRepository
  {
    IEnumerable<Course> GetCourses(Guid authorId);
    Course GetCourse(Guid authorId, Guid courseId);
    void AddCourse(Guid authorId, Course course);
    void UpdateCourse(Course course);
    void DeleteCourse(Course course);
    IEnumerable<Author> GetAuthors();
    IEnumerable<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
    IEnumerable<Author> GetAuthors(string mainCategory, string search);
    Author GetAuthor(Guid authorId);
    IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
    void AddAuthor(Author author);
    void DeleteAuthor(Author author);
    void UpdateAuthor(Author author);
    bool AuthorExists(Guid authorId);
    bool Save();
  }
}
