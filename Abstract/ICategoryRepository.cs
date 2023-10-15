using MepasTask.Models;

namespace MepasTask.Abstract
{
    public interface ICategoryRepository
    {
        public List<CategoryModel> getAllCategories();

        public void addCategory(CategoryModel category);

        public CategoryModel findByName(String name);

        public string findById(string categoryId);
    }
}
