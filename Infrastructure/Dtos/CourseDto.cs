using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos;

public class CourseDto
{
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
