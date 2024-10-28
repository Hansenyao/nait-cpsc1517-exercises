using BookSystem;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookWebApp.Components.Pages
{
    public partial class ReviewCollection
    {
        private string feedback = string.Empty;
        private List<string> errorMsgs = [];
        //
        private string isbn = string.Empty;
        private string title = string.Empty;
        private string selectedAuthor = string.Empty;
        private List<string> authors = [];
        //
        private string reviewer = string.Empty;
        private RatingType rating = RatingType.MustHave;
        private string comment = string.Empty;

        // Inject a service to prompt message to user
        [Inject] IJSRuntime JSRuntime { get; set; }

        // Inject a service to navigate pages
        [Inject] NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            authors = Author.Author_GetList();
            base.OnInitialized();
        }
        
        private void OnCollect()
        {
            // Reset
            feedback = string.Empty;
            errorMsgs.Clear();

            // Validation
            if (string.IsNullOrWhiteSpace(isbn))
            {
                errorMsgs.Add("ISBN is required.");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                errorMsgs.Add("Title is required.");
            }
            if (selectedAuthor == string.Empty)
            {
                errorMsgs.Add("Author is required.");
            }
            if (string.IsNullOrEmpty(reviewer))
            {
                errorMsgs.Add("Reviewer is required.");
            }
            if (string.IsNullOrEmpty(comment))
            {
                errorMsgs.Add("Comment is required.");
            }

            // Return directly if there are any errors
            if (errorMsgs.Count > 0) {
                return;
            }

            try
            {
                // Construct a new review object instance
                Review review = new Review(isbn, title, selectedAuthor, reviewer, rating, comment);

                // Save the new review to CSV in a new line
                const string CSV_FILE_NAME = @"Data/TestData.csv";
                string reviewLine = $"{review}\n";
                File.AppendAllText(CSV_FILE_NAME, reviewLine);
                feedback = $"New review: {review} has saved to file.";
            }
            catch (ArgumentNullException ex)
            {
                errorMsgs.Add($"Missing data: {GetInnerException(ex).Message}");
            }
            catch (ArgumentException ex)
            {
                errorMsgs.Add($"Invalid data value: {GetInnerException(ex).Message}");
            }
            catch (FormatException ex)
            {
                errorMsgs.Add($"Invalid data format: {GetInnerException(ex).Message}");
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"System error: {GetInnerException(ex).Message}");
            }
        }

        private async void OnClearForm()
        {
            object[] messageLine = new object[] { "All unsaved data will loss. Are you sure you want to clear the form?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageLine))
            {

                feedback = string.Empty;
                errorMsgs.Clear();
                //
                isbn = string.Empty;
                title = string.Empty;
                selectedAuthor = string.Empty;
                reviewer = string.Empty;
                rating = RatingType.MustHave;
                comment = string.Empty;

                // Update elements in UI
                await InvokeAsync(StateHasChanged);
            }
        }

        private void OnGoToReport()
        {
            NavigationManager.NavigateTo("reviewreport");
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
