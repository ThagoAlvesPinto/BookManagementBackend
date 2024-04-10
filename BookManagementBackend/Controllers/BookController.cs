using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBooksService booksService;

        public BookController(IBooksService _booksService)
        {
            booksService = _booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(string isbn)
        {
            Books? book = await booksService.GetBookByIsbn(isbn);

            if (book is not null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound("Livro não Encontrado.");
            }
        }

        [HttpPost("Return")]
        public async Task<IActionResult> ReturnBook(BookReturnRequest request)
        {
            if (await booksService.ReturnBook(request.BookId, request.ReturnUserName))
            {
                return Ok("Livro devolvido com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao devolver livro.");
            }
        }
    }
}
