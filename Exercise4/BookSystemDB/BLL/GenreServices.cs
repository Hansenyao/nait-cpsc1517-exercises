using BookSystemDB.DAL;
using BookSystemDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystemDB.BLL
{
    public class GenreServices
    {
        private eBooksContext _context;
        public void GenreService(eBooksContext context)
        {
            _context = context;
        }

        #region Queriese
        public List<Genre> GetGenres()
        {
            return _context.Genres.OrderBy(x => x.Description).ToList();
        }
        #endregion
    }
}
