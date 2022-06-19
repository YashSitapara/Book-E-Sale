using BookStore.models.Models;
using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookRepository:BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex,int pageSize,string keyword)
        {
            if(pageIndex > 0)
            {
                keyword = keyword?.ToLower()?.Trim();
                var query = _context.Books.Where(q =>keyword==null || q.Name.ToLower().Contains(keyword) || q.Description.ToLower().Contains(keyword)).AsQueryable();
                var totalCount = query.Count();
                List<Book> booklist = query.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
                return new ListResponse<Book>()
                {
                    records = booklist,
                    totalRecords = totalCount
                };
            }
            return null;
        }

        public Book GetBook(int id)
        {
            if(id > 0)
            {
                return _context.Books.Where(q => q.Id == id).FirstOrDefault();
            }
            return null;
        }

        public Book AddBook(Book model)
        {
            var entry = _context.Books.Add(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool UpdateBook(Book model)
        {
            BookRepository _repository = new BookRepository();
            var book = _repository.GetBook(model.Id);
            if (book == null)
                return false;
            _context.Books.Update(model);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(int id)
        {
            BookRepository _repository = new BookRepository();
            var book = _repository.GetBook(id);
            if (book == null)
                return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
