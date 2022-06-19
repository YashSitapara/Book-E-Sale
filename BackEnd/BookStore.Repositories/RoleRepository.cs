using BookStore.models.Models;
using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class RoleRepository:BaseRepository
    {
        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            if (pageIndex > 0)
            {
                keyword = keyword?.ToLower().Trim();
                var query = _context.Roles.Where(q => keyword == null || q.Name.ToLower().Contains(keyword)).AsQueryable();
                var totalCount = query.Count();
                List<Role> rolelist = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new ListResponse<Role>()
                {
                    records = rolelist,
                    totalRecords = totalCount
                };
            }
            return null;
        }

        public Role GetRole(int id)
        {
            if (id > 0)
            {
                return _context.Roles.Where(q => q.Id == id).FirstOrDefault();
            }
            return null;
        }

        public Role AddRole(Role model)
        {
            var entry = _context.Roles.Add(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool UpdateRole(Role model)
        {
            RoleRepository _repository = new RoleRepository();
            var role = _repository.GetRole(model.Id);
            if (role == null)
                return false;
            _context.Roles.Update(model);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteRole(int id)
        {
            RoleRepository _repository = new RoleRepository();
            var role = _repository.GetRole(id);
            if (role == null)
                return false;
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}
