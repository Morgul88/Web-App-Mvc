using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace Infrastructure.Context;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<AddressEntities> Adresses { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<SubscriberEntity> Subscribers { get; set; }

    public DbSet<SavedCoursesEntities> SavedCourses { get; set; }




}
