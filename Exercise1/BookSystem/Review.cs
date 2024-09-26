/*
 *  File Name: Review.cs
 *  Description: The class file for Review which contains the characteristics for a review.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-17
 * 
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    /*
     * Class Name: Review
     * Description: This class contains the characteristics for a review.
     *      ISBN (e.g.: "978-0-7653-8669-4") - Identifies the book identificate number.
     *      Reviewer (e.g: instance of reviewer) - Identifies the reviewer.
     *      Rating (e.g. "Buy") - Identifies the enum value of the reviewer recommendation.
     *      Comment (e.g. "... great read for a lazy day by the pool") - Reviewer comment.
     * 
     **/
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
                // Comment can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Commnent is required.");
                }
                _comment = value.Trim();
            }
        }
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                // ISBN can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("ISBN is required.");
                }
                _isbn = value.Trim();
            }
        }
        public Reviewer Reviewer
        {
            get { return _reviewer; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Reviewer is required.");
                }
                _reviewer = value;
            }
        }
        public RatingType Rating { get; set; }
        #endregion //Properties

        #region Constructors
        public Review(string isbn, Reviewer reviewer, RatingType rating, string comment)
        {
            if (!Enum.IsDefined(typeof(RatingType), rating))
            {
                throw new ArgumentException($"RatingType {rating} is invalid.");
            }
            ISBN = isbn;
            Reviewer = reviewer;
            Rating = rating;
            Comment = comment; 
        }
        #endregion //

        #region Methods
        public override string ToString()
        {
            return $"{ISBN},{Reviewer},{Rating},{Comment}";
        }
        #endregion //Methods
    }
}
