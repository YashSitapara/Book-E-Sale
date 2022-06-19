using BookStore.Models.Models;
using BookStore.models.ViewModels;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStore.Repositories
{
    public class orderRepository : BaseRepository
    {
        public Order add(Order model)
        {
            var response = _context.Orders.Add(model);
            _context.SaveChanges();
            return response.Entity;
        }
    }
}