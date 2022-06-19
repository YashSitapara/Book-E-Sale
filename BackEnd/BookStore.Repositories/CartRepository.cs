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
    public class CartRepository:BaseRepository
    {
        public ListResponse<GetCartModel> GetCartItems(int pageIndex,int pageSize,int userId)
        {
            if (pageIndex > 0)
            {
                var query = _context.Carts.AsQueryable();
                ListResponse<GetCartModel> result = new ListResponse<GetCartModel>();
                List<GetCartModel> getCartModels = new List<GetCartModel>();

                query = query.Where(cart => cart.Userid == userId).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                foreach (Cart cart in query.ToList())
                {
                    Book book = _context.Books.Where(b => b.Id == cart.Bookid).FirstOrDefault();
                    BookModel bookModel = new BookModel(book);
                    GetCartModel getCartModel = new GetCartModel()
                    {
                        Id = cart.Id,
                        Userid = cart.Userid,
                        Book = bookModel,
                        Quantity = cart.Quantity,
                    };
                    getCartModels.Add(getCartModel);
                }
                result.records = getCartModels;
                result.totalRecords = getCartModels.Count;
                return result;
            }
                return null;
        }

        public Cart GetCartItem(int id)
        {
            if (id > 0)
            {
                return _context.Carts.FirstOrDefault(c => c.Id == id);
            }
            return null;
        }

        public Cart AddCartItem(Cart cart)
        {
            if (_context.Carts.Any(c => c.Id != cart.Id && c.Bookid == cart.Bookid && c.Userid == cart.Userid))
                throw new Exception($"Book with id{cart.Bookid} already added in cart. Update the quantity of added item in the cart.");
            else
            {
                var entry = _context.Carts.Add(cart);
                _context.SaveChanges();
                return entry.Entity;
            }
        }

        public bool UpdateCart(Cart model)
        {
            CartRepository _repository = new CartRepository();
            var cart = _repository.GetCartItem(model.Id);
            cart.Quantity = model.Quantity;
            _context.Carts.Update(cart);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCart(int id)
        {
            CartRepository _repository = new CartRepository();
            var cart = _repository.GetCartItem(id);
            if (cart == null)
                return false;
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
