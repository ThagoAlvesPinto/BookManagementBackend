using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository booksRepository;
        private readonly IGoogleAPIService googleAPIService;
        private readonly IOpenLibraryService openLibraryService;
        private readonly IBooksReturnRepository booksReturnRepository;

        public BooksService(IBooksRepository _booksRepository,
                            IGoogleAPIService _googleAPIService,
                            IOpenLibraryService _openLibraryService,
                            IBooksReturnRepository _booksReturnRepository)
        {
            booksRepository = _booksRepository;
            googleAPIService = _googleAPIService;
            openLibraryService = _openLibraryService;
            booksReturnRepository = _booksReturnRepository;
        }

        public async Task<Books?> GetBookByIsbn(string isbn)
        {
            Books? internalBook =  await booksRepository.GetBook(isbn);

            if (internalBook is not null) return internalBook;

            GoogleBook? externalBook = await googleAPIService.GetGoogleBookByIsbn(isbn);

            if (externalBook?.VolumeInfo is null) return null;

            Books book = new()
            {
                Author = String.Join(", ", externalBook.VolumeInfo.Authors ?? []),
                Description = externalBook.VolumeInfo.Description,
                Genre = String.Join(", ", externalBook.VolumeInfo.Categories ?? []),
                ImageLink = await openLibraryService.GetImageCoverByNameAndAuthor(externalBook.VolumeInfo.Title ?? "", externalBook.VolumeInfo.Authors?[0] ?? ""),
                Isbn10 = externalBook.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_10")?.Identifier,
                Isbn13 = externalBook.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_13")?.Identifier,
                Language = externalBook.VolumeInfo.Language,
                Pages = externalBook.VolumeInfo.PageCount,
                PublishedDate = externalBook.VolumeInfo.PublishedDate,
                Publisher = externalBook.VolumeInfo.Publisher,
                Title = externalBook.VolumeInfo.Title
            };

            await booksRepository.AddBook(book);

            return await booksRepository.GetBook(isbn);
        }

        public async Task<bool> ReturnBook(int bookId, string returnUserName)
        {
            try
            {
                BooksReturn booksReturn = new(bookId, DateTime.Now, returnUserName);

                await booksReturnRepository.AddBookReturn(booksReturn);

                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
