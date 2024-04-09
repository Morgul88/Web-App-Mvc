using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public class CourseFactory
{
    public static Course Create(CourseEntity entity)
    {
        try
        {
            return new Course
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                OrginalPrice = entity.OrginalPrice,
                DiscountPrice = entity.DiscountPrice,
                Hours = entity.Hours,
                IsBestSeller = entity.IsBestSeller,
                LikesInNumber = entity.LikesInNumber,
                LikesInProcent = entity.LikesInProcent,
                ImageName = entity.ImageName,
                Category = entity.Category,
                
            };
        }
        catch (Exception ex)
        {
            return null!;
        }
    }

    public static IEnumerable<Course> Create(List<CourseEntity> entities) 
    {
        List<Course> courses = new List<Course>();

        try
        {
            foreach (var entity in entities)
                courses.Add(Create(entity));
        }
        catch { }
            
        return courses;
    }
}
