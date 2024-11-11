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
        private List<Genre> genreList = [];

        [Inject] GenreServices genreServices { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                genreList = genreServices.GetGenres();
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Data Loading Error: {GetInnerException(ex).Message}");
            }

            base.OnInitialized();
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
