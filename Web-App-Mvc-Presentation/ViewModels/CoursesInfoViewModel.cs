using Infrastructure.Dtos;
using Infrastructure.Entities;
using Web_App_Mvc_Presentation.Models;

namespace Web_App_Mvc_Presentation.ViewModels;

public class CoursesInfoViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];

    public IEnumerable<CourseViewModel> Coursesview { get; set; } = [];

    public IEnumerable<ContactEntity> contacts { get; set; } = [];
}
