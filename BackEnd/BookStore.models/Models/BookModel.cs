using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.models.Models
{
    public class BookModel
    {
        public BookModel() { }

        public BookModel(Book model)
        {
            id = model.Id;
            name = model.Name;
            price = model.Price;
            description = model.Description;
            base64image = model.Base64image;
            categoryId = model.Categoryid;
            publisherId = model.Publisherid;
            quantity = model.Quantity;
        }

        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public string? base64image { get; set; }
        public int categoryId { get; set; }
        public int publisherId { get; set; }
        public int quantity { get; set; }
    }
}
