using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO, CancellationToken cancellationToken)
        {
            var newAuthor = await _authorService.AddAsync(authorDTO, cancellationToken);

            return CreatedAtAction(nameof(GetAuthor), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAuthors(CancellationToken cancellationToken)
        {
            var authors = await _authorService.GetAllAsync(cancellationToken);

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetByIdAsync(id, cancellationToken);

            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorDTO author,
            CancellationToken cancellationToken)
        {
            var updated = await _authorService.UpdateAsync(author, cancellationToken);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            await _authorService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpGet("with-book-count")]
        public async Task<IActionResult> GetAuthorsWithBookCount(CancellationToken cancellationToken)
        {
            var authors = await _authorService.GetAuthorsWithCountBookAsync(cancellationToken);
            return Ok(authors);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAuthorByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var author = await _authorService.GetAuthorByNameAsync(name, cancellationToken);
            if (author is null)
                return NotFound($"Author with name containing '{name}' not found.");

            return Ok(author);
        }

    }
}
