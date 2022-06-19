using BookStore.models.Models;
using BookStore.models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    
    public class UserRepository:BaseRepository
    {
        
        public ListResponse<User> GetUsers(int pageIndex,int pageSize,string keyword)
        {
            
            if (pageIndex > 0)
            {
                keyword = keyword?.ToLower().Trim();
                var query = _context.Users.Where(q =>  keyword == null || q.Firstname.ToLower().Contains(keyword) || q.Lastname.ToLower().Contains(keyword)).AsQueryable();
                int totalCount= query.Count();
                List<User> userList = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return new ListResponse<User>()
                {
                    records = userList,
                    totalRecords = totalCount
                };
            }
            return null;
        }

        public User Login(User user)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(user.Email.ToLower()) && c.Password.Equals(user.Password));
        }

        public User Register(User user)
        {
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public User GetUser(int id)
        {
            if (id > 0)
            {
                return _context.Users.Where(q => q.Id == id).FirstOrDefault();
            }
            return null;
        }

        public User UpdateUser(User model)
        {
            UserRepository _repository = new UserRepository();
            var user = _repository.GetUser(model.Id);
            if (user == null)
            {
                return null;
            }

            var entry = _context.Update(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteUser(int id)
        {
            UserRepository _repository = new UserRepository();
            var user = _repository.GetUser(id);
            if (user == null)
                return false;
            
            _context.Remove(user);
            _context.SaveChanges();
            return true;
            
        }
    }

    
}
