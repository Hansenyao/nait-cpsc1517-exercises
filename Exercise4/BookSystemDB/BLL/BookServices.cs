using BookSystemDB.DAL;
using BookSystemDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystemDB.BLL
{
    public class BookServices
    {
        private eBooksContext _context;

        public BookServices(eBooksContext context)
        {
            _context = context;
        }

        public List<Book> GetBooksByGenre(int genreId)
        {
            return _context.Books.Where(x => x.GenreId == genreId).ToList();
        }
    }
}
