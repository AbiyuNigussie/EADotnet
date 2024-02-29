using EventAddis.Entity;
using EventAddis.Repository;
using WebService.API.Data;

namespace EventAddis.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context; 
        }

        public bool CreateCategory(Category categoryCreate)
        {
            _context.Categories.Add(categoryCreate);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();

            return categories;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
