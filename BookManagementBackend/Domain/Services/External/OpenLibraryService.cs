using BookManagementBackend.Domain.Interfaces.Services.External;
using Newtonsoft.Json.Linq;

namespace BookManagementBackend.Domain.Services.External
{
    public class OpenLibraryService : IOpenLibraryService
    {
        public async Task<string?> GetImageCoverByNameAndAuthor(string name, string author)
        {
            string formattedTitle = name.Replace(" ", "+");
            string formattedAuthor = author.Replace(" ", "+");

            string baseUrl = "http://openlibrary.org/search.json?";

            string urlParameters = $"title={formattedTitle}&author={formattedAuthor}";

            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(baseUrl + urlParameters);
            using HttpContent content = response.Content;

            string data = await content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                JObject result = JObject.Parse(data);

                if (result["num_found"] is not null && (int?)result["num_found"] > 0)
                {
                    string? coverLink = result["docs"]?[0]?["cover_i"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(coverLink))
                    {
                        return $"http://covers.openlibrary.org/b/id/{coverLink}-M.jpg";
                    }
                }
            }

            return null;
        }
    }
}
