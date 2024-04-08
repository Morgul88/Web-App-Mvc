using System.ComponentModel.DataAnnotations;

namespace Web_App_Mvc_Presentation.ViewModels;

public class CourseViewModel
{
    [Required(ErrorMessage = "A title is required")]
    [Display(Name = "Title", Prompt = "Title")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "A image name is required")]
    [Display(Name = "Image name", Prompt = "Image name")]
    public string? ImageName { get; set; }

    [Required(ErrorMessage = "A author is required")]
    [Display(Name = "Author", Prompt = "Author")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "required")]
    [Display(Name = "Is best seller", Prompt = "Is best seller?")]
    public bool IsBestSeller { get; set; }

    [Required(ErrorMessage = "A hour is required")]
    [Display(Name = "Hour", Prompt = "Hour")]
    public int Hours { get; set; }

    [Required(ErrorMessage = "A OrginalPrice is required")]
    [Display(Name = "OrginalPrice", Prompt = "OrginalPrice")]
    public decimal OrginalPrice { get; set; }

    [Required(ErrorMessage = "A DiscountPrice is required")]
    [Display(Name = "DiscountPrice", Prompt = "DiscountPrice")]
    public decimal DiscountPrice { get; set; }

    [Required(ErrorMessage = "A LikesInProcent is required")]
    [Display(Name = "LikesInProcent", Prompt = "LikesInProcent")]
    public decimal LikesInProcent { get; set; }

    [Required(ErrorMessage = "A LikesInNumber is required")]
    [Display(Name = "LikesInNumber", Prompt = "LikesInNumber")]
    public decimal LikesInNumber { get; set; }

    [Required(ErrorMessage = "A Category is required")]
    [Display(Name = "Category", Prompt = "Category")]
    public string? Category { get; set; }
}
