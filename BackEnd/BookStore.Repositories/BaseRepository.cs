using BookStore.models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BaseRepository
    {
        protected readonly postgresContext _context = new postgresContext();
    }
}
