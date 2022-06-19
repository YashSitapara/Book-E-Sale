using BookStore.models.Models;
using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class CategoryRepository:BaseRepository
    {
        public ListResponse<Category> GetCategories(int pageIndex, int pageSize, string keyword)
        {
            if (pageIndex > 0)
            {
                keyword = keyword?.ToLower().Trim();
                var query = _context.Categories.Where(q => keyword == null || q.Name.ToLower().Contains(keyword)).AsQueryable();
                var totalCount= query.Count();
                List<Category> categorylist = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return new ListResponse<Category>()
                {
                    records = categorylist,
                    totalRecords = totalCount
                };
            }
            return null;
        }

        public Category GetCategory(int id)
        {
            if (id > 0)
            {
                return _context.Categories.FirstOrDefault(c => c.Id == id);
            }
            return null;
        }

        public Category AddCategory(Category category)
        {
            var entry = _context.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool UpdateCategory(Category model)
        {
            CategoryRepository _repository = new CategoryRepository();
            var category = _repository.GetCategory(model.Id);
            if (category == null)
                return false;
            _context.Categories.Update(model);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(int id)
        {
            CategoryRepository _repository = new CategoryRepository();
            var category = _repository.GetCategory(id);
            if (category == null)
                return false;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
