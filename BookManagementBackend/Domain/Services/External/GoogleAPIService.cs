using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Models.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookManagementBackend.Domain.Services.External
{
    public class GoogleAPIService : IGoogleAPIService
    {
        public async Task<GoogleBook?> GetGoogleBookByIsbn(string isbn)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"volumes?q=isbn:{isbn}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                GoogleBooksResponse gBookResponse = JsonConvert.DeserializeObject<GoogleBooksResponse>(result) ?? new();

                return gBookResponse.Items?.FirstOrDefault();
            }
            return null;
        }
    }
}
