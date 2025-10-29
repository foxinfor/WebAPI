using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO book, CancellationToken cancellationToken)
        {
            var newBook = await _bookService.AddAsync(book, cancellationToken);

            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
        {
            var books = await _bookService.GetAllAsync(cancellationToken);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(id, cancellationToken);

            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO book,
            CancellationToken cancellationToken)
        {
            var update = await _bookService.UpdateAsync(book, cancellationToken);

            return Ok(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            await _bookService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpGet("after-year/{year}")]
        public async Task<IActionResult> GetBooksAfterYear(int year, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksAfterYearAsync(year, cancellationToken);
            return Ok(books);
        }

    }
}
