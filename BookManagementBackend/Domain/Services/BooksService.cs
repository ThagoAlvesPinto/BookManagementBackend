using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Extensions;
using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Domain.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IGoogleAPIService _googleAPIService;
        private readonly IOpenLibraryService _openLibraryService;
        private readonly IBooksReturnRepository _booksReturnRepository;

        public BooksService(IBooksRepository booksRepository,
                            IGoogleAPIService googleAPIService,
                            IOpenLibraryService openLibraryService,
                            IBooksReturnRepository booksReturnRepository)
        {
            _booksRepository = booksRepository;
            _googleAPIService = googleAPIService;
            _openLibraryService = openLibraryService;
            _booksReturnRepository = booksReturnRepository;
        }

        public async Task<ServiceResult<Books>> GetBookByIsbn(string isbn)
        {
            try
            {
                Books? book = await _booksRepository.GetBook(isbn);

                if (book is null)
                    return new(false, "Livro não encontrado.");

                return new(book);
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }            
        }

        public async Task<ServiceResult<ExternalBookResponse>> GetExternalBook(string isbn)
        {
            try
            {
                GoogleBook? externalBook = await _googleAPIService.GetGoogleBookByIsbn(isbn);

                if (externalBook?.VolumeInfo is null)
                    return new(false, "Livro não encontrado.");

                ExternalBookResponse book = new()
                {
                    Author = String.Join(", ", externalBook.VolumeInfo.Authors ?? []),
                    Description = externalBook.VolumeInfo.Description,
                    Genre = String.Join(", ", externalBook.VolumeInfo.Categories ?? []),
                    ImageLink = await _openLibraryService.GetImageCoverByNameAndAuthor(externalBook.VolumeInfo.Title ?? "", externalBook.VolumeInfo.Authors?[0] ?? ""),
                    Isbn10 = externalBook.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_10")?.Identifier,
                    Isbn13 = externalBook.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_13")?.Identifier,
                    Language = externalBook.VolumeInfo.Language,
                    Pages = externalBook.VolumeInfo.PageCount,
                    PublishedDate = externalBook.VolumeInfo.PublishedDate,
                    Publisher = externalBook.VolumeInfo.Publisher,
                    Title = externalBook.VolumeInfo.Title
                };

                return new(book);
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }            
        }

        public async Task<ServiceResult<List<Books>>> GetBooks()
        {
            try
            {
                List<Books> books = await _booksRepository.GetAllBooks();

                return new(books);
            }
            catch (Exception ex)
            {
                return new(false, ex.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> AddBook(AddBookRequest bookReq)
        {
            try
            {
                bool bookExists = false;

                if (!string.IsNullOrWhiteSpace(bookReq.Isbn13))
                    bookExists = await _booksRepository.BookExists(bookReq.Isbn13);
                else if (!string.IsNullOrWhiteSpace(bookReq.Isbn10))
                    bookExists = await _booksRepository.BookExists(bookReq.Isbn10);

                if (bookExists)
                    return new(false, "Livro já cadastrado.");

                Books book = new()
                {
                    Author = bookReq.Author,
                    Description = bookReq.Description,
                    Genre = bookReq.Genre,
                    ImageLink = bookReq.ImageLink,
                    Isbn10 = bookReq.Isbn10,
                    Isbn13 = bookReq.Isbn13,
                    Language = bookReq.Language,
                    Pages = bookReq.Pages,
                    PublishedDate = bookReq.PublishedDate,
                    Publisher = bookReq.Publisher,
                    Title = bookReq.Title
                };

                await _booksRepository.AddBook(book);

                return new(true);
            }
            catch (Exception e)
            {
                return new(false, e.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> UpdateBook(UpdateBookRequest bookReq)
        {
            try
            {
                if (bookReq.Id is null)
                    return new(false, "Id do livro não informado.");

                Books ? book = await _booksRepository.GetBook(bookReq.Id.Value);

                if (book is null)
                    return new(false, "Livro não encontrado.");

                book.Author = bookReq.Author ?? book.Author;
                book.Description = bookReq.Description ?? book.Description;
                book.Genre = bookReq.Genre ?? book.Genre;
                book.ImageLink = bookReq.ImageLink ?? book.ImageLink;
                book.Isbn10 = bookReq.Isbn10 ?? book.Isbn10;
                book.Isbn13 = bookReq.Isbn13 ?? book.Isbn13;
                book.Language = bookReq.Language ?? book.Language;
                book.Pages = bookReq.Pages ?? book.Pages;
                book.PublishedDate = bookReq.PublishedDate ?? book.PublishedDate;
                book.Publisher = bookReq.Publisher ?? book.Publisher;
                book.Title = bookReq.Title ?? book.Title;

                await _booksRepository.UpdateBook(book);

                return new(true);
            }
            catch (Exception e)
            {
                return new(false, e.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> DeleteBook(int bookId)
        {
            try
            {
                Books? book = await _booksRepository.GetBook(bookId);

                if (book is null)
                    return new(false, "Livro não encontrado.");

                await _booksRepository.DeleteBook(book);

                return new(true);
            }
            catch (Exception e)
            {
                return new(false, e.GetExceptionMessage(), true);
            }
        }

        public async Task<ServiceResult> ReturnBook(int bookId, int userId)
        {
            try
            {
                BooksReturn booksReturn = new()
                {
                    BookId = bookId,
                    ReturnConfirmed = false,
                    ReturnDate = DateTime.Now,
                    ReturnUserId = userId
                };

                await _booksReturnRepository.AddBookReturn(booksReturn);

                return new();
            }
            catch (Exception e)
            {
                return new(false, e.GetExceptionMessage(), true);
            }            
        }        
    }
}
