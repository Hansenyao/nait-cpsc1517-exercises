/*
 *  File Name: Book_Should.cs
 *  Description: The unit test cases file for class Book.
 *  Version: 1.0
 *  
 *  Author: Youfang Yao
 *  Date: 2024-09-26
 * 
 **/
using BookSystem;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BooksUnitTestEx2
{
    public class Book_Should
    {
        #region Constructor
        [Fact]
        public void create_book_without_review()
        {
            //given - arrange
            string expectedISBN = "978-0-7653-8669-4";
            string expectedTitle = "Book Name";
            int expectedPublishYear = 2022;
            GenreType expectedGenre = GenreType.Fiction;
            List<Review>? expectedReviews = null;
            //
            string expectedFirstName = "lowan";
            string expectedLastName = "behold";
            string expectedContactUrl = "lowanbehold.fantasy.ca";
            string expectedCity = "edmonton";
            string expectedCountry = "canada";
            Author expectedAuthor = new Author(expectedFirstName, expectedLastName, expectedContactUrl, expectedCity, expectedCountry);

            //when - act
            Book actual = new Book(expectedISBN, expectedTitle, expectedPublishYear, expectedAuthor, expectedGenre, expectedReviews);

            //then - assert
            actual.ISBN.Should().Be(expectedISBN);
            actual.Title.Should().Be(expectedTitle);
            actual.PublishYear.Should().Be(expectedPublishYear);
            actual.Author.FirstName.Should().Be(expectedFirstName);
            actual.Author.LastName.Should().Be(expectedLastName);
            actual.Author.ContactUrl.Should().Be(expectedContactUrl);
            actual.Author.ResidentCity.Should().Be(expectedCity);
            actual.Author.ResidentCountry.Should().Be(expectedCountry);
            actual.Genre.Should().Be(expectedGenre);
            actual.Reviews.Should().HaveCount(0);
            actual.TotalReviews.Should().Be(0);
        }

        [Fact]
        public void create_book_with_reviews()
        {
            //given - arrange
            string expectedISBN = "978-0-7653-8669-4";
            string expectedTitle = "Book Name";
            int expectedPublishYear = 2022;
            GenreType expectedGenre = GenreType.Fiction;
            //
            string expectedFirstName = "lowan";
            string expectedLastName = "behold";
            string expectedContactUrl = "lowanbehold.fantasy.ca";
            string expectedCity = "edmonton";
            string expectedCountry = "canada";
            Author expectedAuthor = new Author(expectedFirstName, expectedLastName, expectedContactUrl, expectedCity, expectedCountry);
            //
            List<Review> expectedReviews = MakeReviewList();

            //when - act
            Book actual = new Book(expectedISBN, expectedTitle, expectedPublishYear, expectedAuthor, expectedGenre, expectedReviews);

            //then - assert
            actual.ISBN.Should().Be(expectedISBN);
            actual.Title.Should().Be(expectedTitle);
            actual.PublishYear.Should().Be(expectedPublishYear);
            actual.Author.FirstName.Should().Be(expectedFirstName);
            actual.Author.LastName.Should().Be(expectedLastName);
            actual.Author.ContactUrl.Should().Be(expectedContactUrl);
            actual.Author.ResidentCity.Should().Be(expectedCity);
            actual.Author.ResidentCountry.Should().Be(expectedCountry);
            actual.Genre.Should().Be(expectedGenre);
            actual.TotalReviews.Should().Be(expectedReviews.Count);
            actual.Reviews.Should().BeSameAs(expectedReviews);
        }
        [Theory]
        [InlineData(null, "Book Name", 2021, GenreType.Fiction)]
        [InlineData("", "Book Name", 2021, GenreType.Fiction)]
        [InlineData("    ", "Book Name", 2021, GenreType.Fiction)]
        [InlineData("Book ISBN", null, 2021, GenreType.Fiction)]
        [InlineData("Book ISBN", "", 2021, GenreType.Fiction)]
        [InlineData("Book ISBN", "   ", 2021, GenreType.Fiction)]
        [InlineData("Book ISBN", "Book Name", -2010, GenreType.Fiction)]
        [InlineData("Book ISBN", "Book Name", 0, GenreType.Fiction)]
        [InlineData("Book ISBN", "Book Name", 3018, GenreType.Fiction)]
        [InlineData("Book ISBN", "Book Name", 2021, null)]
        [InlineData("Book ISBN", "Book Name", 2021, (GenreType)(-1))]
        public void Throw_Exception_For_Bad_Data_Using_Constructor_With_Author(string isbn, string title, 
            int publishYear, GenreType genre)
        {
            //Arrange
            Author author = new Author("lowan", "behold", "lowanbehold.fantasy.ca", "edmonton", "Canada");

            //Act
            Action action = () => new Book(isbn, title, publishYear, author, genre, MakeReviewList());

            //Assert
            action.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void Throw_Exception_For_Bad_Data_Using_Constructor_Without_Author()
        {
            //Arrange

            //Act
            Action action = () => new Book("978-0-7653-8669-4", "Book Name", 2021, null, GenreType.History);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage("*is required*");
        }
        #endregion

        #region Properties
        [Fact]
        public void Allow_Change_To_PublishYear()
        {
            //Arrange
            Book actual = MakeBook();
            int expectedPublishYear = 1998;

            //Act
            actual.PublishYear = expectedPublishYear;

            //Assert
            actual.PublishYear.Should().Be(expectedPublishYear);
        }
        [Theory]
        [InlineData(-1900)]
        [InlineData(0)]
        [InlineData(3098)]
        public void Throw_Exception_For_Changing_PublishYear(int publishYear)
        {
            //Arrange
            Book actual = MakeBook();

            //Action
            Action action = () => actual.PublishYear = publishYear;

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage("*is invalid*");
        }
        #endregion

        #region Methods
        [Fact]
        public void Allow_Add_The_First_Review()
        {
            //Arrange
            Book actual = MakeBook(false);
            string isbn = actual.ISBN;
            Review review = new Review(isbn, new Reviewer("bob", "John", "bob.j@gmail.com"), RatingType.EasyReading, "Easy to read.");

            //Act
            actual.AddReview(isbn, review);

            //Assert
            actual.TotalReviews.Should().Be(1);
            actual.Reviews.Should().Contain(review);
        }
        [Fact]
        public void Allow_Add_Other_Reviews()
        {
            //Arrange
            Book actual = MakeBook();
            int countBeforeAdd = actual.Reviews.Count;
            string isbn = actual.ISBN;
            Review review = new Review(isbn, new Reviewer("bob", "John", "bob.j@gmail.com"), RatingType.EasyReading, "Easy to read.");
        
            //Act
            actual.AddReview(isbn, review);

            //Assert
            actual.TotalReviews.Should().Be(countBeforeAdd + 1);
            actual.Reviews.Should().Contain(review);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Throw_Exception_For_Add_Review_Without_ISBN(string isbn)
        {
            //Arrage
            Book actual = MakeBook();
            Review review = new Review(actual.ISBN, new Reviewer("bob", "John", "bob.j@gmail.com"), RatingType.EasyReading, "Easy to read.");

            //Action
            Action action = () => actual.AddReview(isbn, review);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*ISBN is required*");
        }
        [Fact]
        public void Throw_Exception_For_Add_Review_Without_Review()
        {
            //Arrage
            Book actual = MakeBook();
            string isbn = actual.ISBN;

            //Action
            Action action = () => actual.AddReview(isbn, null);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*Review is required*");
        }
        [Fact]
        public void Throw_Exception_For_Add_Review_Which_ISBN_IS_INVALID()
        {
            //Arrage
            Book actual = MakeBook();
            string invalidISBN = actual.ISBN + "-000";
            Review review = new Review(invalidISBN, new Reviewer("bob", "John", "bob.j@gmail.com"), RatingType.EasyReading, "Easy to read.");

            //Action
            Action action = () => actual.AddReview(invalidISBN, review);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage($"*{invalidISBN}*");
        }
        [Fact]
        public void Throw_Exception_For_Add_Review_Which_Has_Duplicated_Reviewer()
        {
            //Arrage
            Book actual = MakeBook();
            string isbn = actual.ISBN;
            string duplicatedFirstName = actual.Reviews[0].Reviewer.FirstName;
            string duplicatedLastName = actual.Reviews[0].Reviewer.LastName;
            Review review = new Review(isbn, new Reviewer(duplicatedFirstName, duplicatedLastName, "xxxx.yyyy@gmail.com"), RatingType.Pass, "It is a good book!");

            //Action
            Action action = () => actual.AddReview(isbn, review);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage($"*{review.Reviewer.ReviewerName}*");
        }
        #endregion

        #region Utilities
        // Return a List<Review> colloction object
        public List<Review> MakeReviewList()
        {
            const string BOOK_ISBN = "978-0-7653-8669-4";
            List<Review> reviews =
            [
                new Review(BOOK_ISBN, new Reviewer("Jackson", "John", "asdfas@nait.ca"), RatingType.Entertaining, "Good"),
                new Review(BOOK_ISBN, new Reviewer("John", "Jackson", "john.j@gmail.com"), RatingType.EasyReading, "Not bad"),
                new Review(BOOK_ISBN, new Reviewer("Tim", "Kim", "ktim@hotmail.com"), RatingType.SummerReading, "Excellent"),
                new Review(BOOK_ISBN, new Reviewer("Tony", "Hanson", "tony.h@nait.ca"), RatingType.MustHave, "Good")
            ];
            return reviews;
        }
        // Return a Book object
        //  withReivews - true, this Book includes some reviews
        //              - false, this Book's reviews is empty
        public Book MakeBook(bool withReivews = true)
        {
            const string BOOK_ISBN = "978-0-7653-8669-4";
            Author author = new Author("lowan", "behold", "lowanbehold.fantasy.ca", "edmonton", "canada");
            List<Review> reviews = MakeReviewList();
            return new Book(BOOK_ISBN, "Book Name", 2022, author, GenreType.Adventure, withReivews ? reviews : null);
        }
        #endregion
    }
}
