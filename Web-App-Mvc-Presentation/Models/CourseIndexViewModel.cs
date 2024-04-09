using Infrastructure.Models;

namespace Web_App_Mvc_Presentation.Models;

public class CourseIndexViewModel
{
    public IEnumerable<Course>? Courses { get; set; }

    public Pagination? Pagination { get; set; }

    public IEnumerable<string>? Categories { get; set; }

}
