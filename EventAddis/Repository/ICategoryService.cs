using EventAddis.Entity;

namespace EventAddis.Repository
{
    public interface ICategoryService
    {
        public ICollection<Category> GetCategories();
        public bool CreateCategory(Category categoryCreate);

        public bool Save();
    }
}
