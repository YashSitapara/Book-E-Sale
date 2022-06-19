using BookStore.models.Models;
using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class publisherRepository:BaseRepository
    {
        public ListResponse<Publisher> GetPublishers(int pageIndex,int pageSize,string keyword)
        {
            if(pageIndex > 0)
            {
                keyword = keyword?.ToLower().Trim();
                var query = _context.Publishers.Where(q => keyword == null || q.Name.ToLower().Contains(keyword)).AsQueryable();
                var totalCount = query.Count();
                List<Publisher> publisherlist= query.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();     
                return new ListResponse<Publisher>()
                {
                    records = publisherlist,
                    totalRecords = totalCount
                };
            }
            return null;
        }

        public Publisher GetPublisher(int id)
        {
            if(id > 0)
            {
                return _context.Publishers.Where(q => q.Id == id).FirstOrDefault();
            }
            return null;
        }

        public Publisher AddPublisher(Publisher publisher)
        {
            var entry = _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool UpdatePublisher(Publisher model)
        {
            publisherRepository _repository = new publisherRepository();
            var publisher = _repository.GetPublisher(model.Id);
            if (publisher == null)
            {
                return false;
            }

            _context.Update(model);
            _context.SaveChanges();
            return true;
            
        }

        public bool DeletePublisher(int id)
        {
            publisherRepository _repository = new publisherRepository();
            var publisher = _repository.GetPublisher(id);
            if (publisher == null)
                return false;
            _context.Remove(publisher);
            _context.SaveChanges();
            return true;
            

        }
    }

    
}
