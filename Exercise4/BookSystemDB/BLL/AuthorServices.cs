using BookSystemDB.DAL;
using BookSystemDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystemDB.BLL
{
    public class AuthorServices
    {
        private eBooksContext _context;

        public AuthorServices(eBooksContext context)
        {
            _context = context;
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }
    }
}
