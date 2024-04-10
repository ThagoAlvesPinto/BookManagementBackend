using Newtonsoft.Json;

namespace BookManagementBackend.Domain.Models.Responses
{
    public class GoogleBooksResponse
    {
        [JsonProperty("items")]
        public List<GoogleBook> Items { get; set; } = new();
    }

    public class GoogleBook
    {
        [JsonProperty("volumeInfo")]
        public VolumeInfo? VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("authors")]
        public List<string>? Authors { get; set; }

        [JsonProperty("publishedDate")]
        public string? PublishedDate { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("publisher")]
        public string? Publisher { get; set; }

        [JsonProperty("categories")]
        public List<string>? Categories { get; set; }

        [JsonProperty("pageCount")]
        public int? PageCount { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("industryIdentifiers")]
        public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
    }

    public class IndustryIdentifier
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }
    }
}
