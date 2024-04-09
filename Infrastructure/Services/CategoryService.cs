using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>?> GetAllCategoriesAsync()
        {
            var categories = await _context.Courses
                .Select(x => x.Category)
                .Distinct()
                .ToListAsync();

            return categories.AsEnumerable()!;
        }
    }
}