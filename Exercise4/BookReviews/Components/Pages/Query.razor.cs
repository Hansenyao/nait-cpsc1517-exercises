using BookSystemDB.BLL;
using BookSystemDB.Entities;
using Microsoft.AspNetCore.Components;

namespace BookReviews.Components.Pages
{
    public partial class Query
    {
        private string feedback = string.Empty;
        private List<string> errorMsgs = [];
        private int selectedGenreId;
        //
        private List<Genre> genreList = [];
        private List<Author> authorList = [];
        private List<Book> bookList = [];
        private bool noBooks = false;

        // Injecting all services here!
        [Inject] GenreServices genreServices { get; set; }
        [Inject] AuthorServices authorServices { get; set; }
        [Inject] BookServices bookServices { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                // Initialize genres description list and authors list
                genreList = genreServices.GetGenres();
                authorList = authorServices.GetAuthors();
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Data Loading Error: {GetInnerException(ex).Message}");
            }

            base.OnInitialized();
        }

        public void OnLoadBookByGenre()
        {
            feedback = string.Empty;
            errorMsgs.Clear();
            noBooks = false;

            try
            {
                // Show an error message if no selected genre
                if (selectedGenreId == 0)
                {
                    errorMsgs.Add("Select a genre before fetching your books.");
                }
                // Load books which belong to the selected genre
                else
                {
                    bookList = bookServices.GetBooksByGenre(selectedGenreId);
                }
                if (bookList.Count == 0)
                {
                    noBooks = true;
                }
                else
                {
                    feedback = "View query results";
                }
            }
            catch ( Exception ex )
            {
                errorMsgs.Add($"Data Loading Error: {GetInnerException(ex).Message}");
            }
        }

        public async void OnClear()
        {
            // clear selecting and table
            selectedGenreId = 0;
            feedback = string.Empty;
            errorMsgs.Clear();
            bookList.Clear();
            noBooks = false;

            // Re-render page with new datas (in an async mode)
            await InvokeAsync(StateHasChanged);
        }

        /*
         *  Return an author's full name by specialized authorId
         */
        public string GetAuthorFullName(int authorId)
        {
            Author? author = authorList.Find(x=>x.AuthorId == authorId);
            if (author == null)
            {
                return string.Empty;
            }
            else
            {
                return author.FullName;
            }
        }

        private Exception GetInnerException(Exception ex)
        {
            //drill down into your Exception until there are no more inner exceptions
            //at this point you have the "real" error
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
