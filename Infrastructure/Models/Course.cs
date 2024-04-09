namespace Infrastructure.Models;

public class Course
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ImageName { get; set; }

    public string? Author { get; set; }

    public bool IsBestSeller { get; set; }

    public int Hours { get; set; }

    public decimal OrginalPrice { get; set; }

    public decimal DiscountPrice { get; set; }

    public decimal LikesInProcent { get; set; }

    public decimal LikesInNumber { get; set; }

    public string? Category { get; set; }

   
}
