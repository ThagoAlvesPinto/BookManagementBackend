namespace BookManagementBackend.Domain.Interfaces.Services.External
{
    public interface IOpenLibraryService
    {
        public Task<string?> GetImageCoverByNameAndAuthor(string name, string author);
    }
}
