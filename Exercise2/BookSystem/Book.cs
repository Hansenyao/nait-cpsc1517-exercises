/*
 *  File Name: Book.cs
 *  Description: The class file for Book which relates to a single author.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-27
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ISBN is required.");
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
                    throw new ArgumentException("Publish year must be a positive and non-zero whole number.");
                }
                // Publish year cannot be a year in the future
                if (value > DateTime.Today.Year)
                {
                    throw new ArgumentException("Publish year cannot be a year in the future.");
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
                if (string.IsNullOrEmpty(value)) 
                {
                    throw new ArgumentException("Title is required.");
                }
                _title = value.Trim(); 
            }
        }

        // Returns the number of reviews for this book in the List<Review> collection.
        // (Read-Only Property)
        public int TotalReviews => Reviews.Count;
        #endregion // Properties

        #region Constructors
        public Book(string isbn, string title, int publishYear, Author author, GenreType genre, List<Review> reviews)
        {
            ISBN = isbn;
            Title = title;
            PublishYear = publishYear;
            Author = author;
            Genre = genre;
            Reviews = reviews;
        }
        #endregion // Constructors

        #region Methods
        public void AddReview(string isbn, Review review)
        {
            // Arguments cannot be null
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentException("ISBN is required.");
            }
            if (review is null)
            {
                throw new ArgumentException("Review is required.");
            }

            // The ISBN must be same as this book's
            if (!ISBN.Equals(isbn.Trim()))
            {
                throw new ArgumentException("ISBN is not for this book.");
            }
            Reviews.Add(review);
        }
        #endregion // Methods
    }
}
