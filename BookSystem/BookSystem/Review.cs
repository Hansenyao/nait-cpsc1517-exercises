using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    public class Review
    {
        #region Data members
        private string _comment = string.Empty;
        private string _isbn = string.Empty;
        private Reviewer _reviewer;
        #endregion //Data members

        #region Properties
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                _isbn = value;
            }
        }
        public Reviewer Reviewer
        {
            get { return _reviewer; }
            set
            {
                _reviewer = value;
            }
        }
        public RatingType Rating { get; set; }
        #endregion //Properties

        #region Constructors
        public Review(string isbn, Reviewer reviewer, RatingType rating, string comment)
        {
            ISBN = isbn;
            Reviewer = reviewer;
            Rating = rating;
            Comment = comment; 
        }
        #endregion //

        #region Methods
        public override string ToString()
        {
            return "";
        }
        #endregion //Methods
    }
}
