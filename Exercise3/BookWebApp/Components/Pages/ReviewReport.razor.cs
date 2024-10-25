using BookSystem;

namespace BookWebApp.Components.Pages
{
    public partial class ReviewReport
    {
        private List<string> errorMsgs = [];
        private List<Review> reviews = [];

        protected override void OnInitialized()
        {
            const string CSV_FILE_NAME = @"Data/TestData-bad.csv";

            ReadReviewsFromFile(CSV_FILE_NAME);

            base.OnInitialized();
        }

        /*
         *  To read all review data from a CSV file
         *  and parse each line string to a review object.
         *  All review objects will be saved in reviews.
         *  
         */
        private void ReadReviewsFromFile(string filePath)
        {
            // Reset error message list
            errorMsgs.Clear();

            // Try to read all review data from CSV file
            string[] reviewData = [];
            try
            {
                reviewData = File.ReadAllLines(filePath);
            }
            catch (FileNotFoundException ex) 
            { 
                errorMsgs.Add($"File {filePath} does not exist. Detail Info: {GetInnerException(ex).Message}");
                return;
            }
            catch (Exception ex)
            {
                errorMsgs.Add($"Failed to read file {filePath}. Detail Info: {GetInnerException(ex).Message}");
                return;
            }

            // Try to parse lines and construct Review objects
            int itemIndex = 0;
            foreach (string line in reviewData)
            {
                try
                {
                    itemIndex++;
                    reviews.Add(Review.Parse(line));
                }
                catch (Exception ex)
                {
                    errorMsgs.Add($"Record Error: {itemIndex}: {GetInnerException(ex).Message}");
                }
            }
        }
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
