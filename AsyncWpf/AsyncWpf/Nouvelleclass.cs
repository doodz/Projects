using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncWpf
{
    public class Nouvelleclass
    {
        private class ResultsTextBox
        {
            public string Text;
        }

        private ResultsTextBox resultsTextBox = new ResultsTextBox();

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            // ONE
            Task<int> getLengthTask = AccessTheWebAsync();

            // FOUR
            int contentLength = await getLengthTask;

            // SIX
            resultsTextBox.Text +=
                $"\r\nLength of the downloaded string: {contentLength}.\r\n";
        }


        async Task<int> AccessTheWebAsync()
        {
            // TWO
            HttpClient client = new HttpClient();
            Task<string> getStringTask =
                client.GetStringAsync("http://msdn.microsoft.com");

            // THREE                 
            string urlContents = await getStringTask;

            // FIVE
            return urlContents.Length;
        }
    }
}