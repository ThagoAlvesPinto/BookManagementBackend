using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBooksService booksService;

        public BookController(IBooksService _booksService)
        {
            booksService = _booksService;
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<APIResponse<Books>>> GetBook(string isbn)
        {
            ServiceResult<Books> result = await booksService.GetBookByIsbn(isbn);

            if (result.ExceptionGenerated)
                return StatusCode(500, result);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            ServiceResult<IEnumerable<Books>> result = await booksService.GetBooks();

            if (result.ExceptionGenerated)
                return StatusCode(500, result);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("External/{isbn}")]
        public async Task<IActionResult> GetExternalBook(string isbn)
        {
            ServiceResult<ExternalBookResponse> result = await booksService.GetExternalBook(isbn);

            if (result.ExceptionGenerated)
                return StatusCode(500, result);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest book)
        {
            ServiceResult ret = await booksService.AddBook(book);

            if (ret.ExceptionGenerated)
                return StatusCode(500, ret);

            if (!ret.Success)
                return BadRequest(ret);

            return Ok(ret);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateBookRequest book)
        {
            ServiceResult ret = await booksService.UpdateBook(book);

            if (ret.ExceptionGenerated)
                return StatusCode(500, ret);

            if (!ret.Success)
                return BadRequest(ret);

            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            ServiceResult ret = await booksService.DeleteBook(id);

            if (ret.ExceptionGenerated)
                return StatusCode(500, ret);

            if (!ret.Success)
                return BadRequest(ret);

            return Ok(ret);
        }

        //[HttpPost("Return")]
        //public async Task<IActionResult> ReturnBook(BookReturnRequest request)
        //{
        //    var ret = await booksService.ReturnBook(request.BookId, request.ReturnUserName);

        //    if (ret.Item1)
        //    {
        //        return Ok("Livro devolvido com sucesso.");
        //    }
        //    else
        //    {
        //        return BadRequest("Erro ao devolver livro. " + ret.Item2);
        //    }
        //}
    }
}
