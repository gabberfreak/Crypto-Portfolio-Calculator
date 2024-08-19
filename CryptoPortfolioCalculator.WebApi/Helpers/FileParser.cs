using System.Text;

namespace CryptoPortfolioCalculator.Web.Helpers
{
    public static class FileReader
    {
        public static async Task<string[]> ReadFileAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            var readedFile = result.ToString();

            return readedFile.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
