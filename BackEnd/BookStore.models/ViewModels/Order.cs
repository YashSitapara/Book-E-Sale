using System;
using System.Collections.Generic;

namespace BookStore.models.ViewModels
{
    public partial class Order
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string? Date { get; set; }
        public int[] Cartids { get; set; } = null!;
    }
}
