/*
 *  File Name: Book.cs
 *  Description: The class file for Book which relates to a single author.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-25
 * 
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    /*
     * Class Name: Book
     * Description: This class contains the characteristics for a book.
     *      A Book is related to a single authorand is assigned a unique industry ISBN. 
     *
     **/
    public class Book
    {
        #region Data members
        private Author _author;
        private string _isbn = string.Empty;
        private int _publishYear;
        private string _title = string.Empty;
        #endregion // Data members

        #region Properties
        // This is the author of the Book.
        // Required.
        public Author Author 
        {  
            get { return _author; } 
            set { 
                // Author cannot be null
                if (value == null)
                {
                    throw new ArgumentNullException("Author is required.");
                }
                _author = value; 
            }
        }

        // This is the Genre type category the Book to which the book has been assigned.
        // Auto-implement property)
        public GenreType Genre { get; set; }

        // This identifies the Book.
        // Can not be null.
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                // ISBN cannot be null
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("ISBN is required.");
                }
                _isbn = value.Trim(); 
            }
        }

        //  This is the year the Book was published.
        //  Required format yyyy.
        public int PublishYear
        {
            get { return _publishYear; }
            set 
            { 
                // Publish year must be a positive and non-zero whole number
                if (value <= 0)
                {
                    throw new ArgumentException("Publish year is invalid. Must be a positive and non-zero whole number.");
                }
                // Publish year cannot be a year in the future
                if (value > DateTime.Today.Year)
                {
                    throw new ArgumentException("Publish year is invalid. Cannot be a year in the future.");
                }
                _publishYear = value; 
            }
        }

        // This is a list of all reviews for the Book.
        // This list can be empty as the book is published or may have reviews
        // from individuals given access to a pre-published copy of the book.
        // A reviewer may only have one review per book.
        // (Auto-implement property)
        public List<Review> Reviews { get; set; } = [];

        // This is the title of the Book.
        // Can not be null.
        public string Title
        {
            get { return _title; }
            set 
            {
                // Title cannot be null
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentNullException("Title is required.");
                }
                _title = value.Trim(); 
            }
        }

        // Returns the number of reviews for this book in the List<Review> collection.
        // (Read-Only Property)
        public int TotalReviews => Reviews.Count;
        #endregion // Properties

        #region Constructors
        public Book(string isbn, string title, int publishYear, Author author, GenreType genre, List<Review>? reviews = null)
        {
            ISBN = isbn;
            Title = title;
            PublishYear = publishYear;
            Author = author;

            // genre must be a value of GenreType

            if (!Enum.IsDefined(typeof(GenreType), genre))
            {
                throw new ArgumentException($"GenreType value {genre} is invalid.");
            }
            Genre = genre;

            // Leave Reviews with an empty colloction when no reviews
            if (reviews != null)
            {
                // Business rule 1: The ISBN in each review should be same as this book's
                foreach(Review r in reviews)
                {
                    if (r.ISBN != isbn)
                    {
                        throw new ArgumentException($"Review ISBN {r.ISBN} is invalid.");
                    }
                }
                // Business rule 2: A reviewer can only submit a single review.
                if (reviews.GroupBy(x => x.Reviewer).Any(g => g.Count() > 1))
                {
                    throw new ArgumentException("A reviewer can only submit a single review.");
                }

                Reviews = reviews;
            }
        }
        #endregion // Constructors

        #region Methods
        public void AddReview(string isbn, Review review)
        {
            // Arguments cannot be null
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentNullException("ISBN is required.");
            }
            if (review == null)
            {
                throw new ArgumentNullException("Review is required.");
            }

            // Business rule 1: The ISBN must be same as this book's
            if (!ISBN.Equals(isbn.Trim()) || !review.ISBN.Equals(isbn.Trim()))
            {
                throw new ArgumentException($"Review ISBN {isbn} is invalid.");
            }
            // Business rule 2: A reviewer can only submit a single review.
            if (Reviews.Any(x => x.Reviewer.ReviewerName.Equals(review.Reviewer.ReviewerName)))
            {
                throw new ArgumentException($"A reviewer can only submit a single review. The reviewer {review.Reviewer.ReviewerName} has submitted a review for this book already.");
            }
            Reviews.Add(review);
        }
        #endregion // Methods
    }
}
