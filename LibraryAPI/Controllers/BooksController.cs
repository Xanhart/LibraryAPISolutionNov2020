using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using LibraryAPI.Filters;

namespace LibraryAPI.Controllers
{
    public class BooksController : ControllerBase
    {

        private readonly ILookupBooks _bookLookup;
        private readonly IBookCommands _bookCommands;

        private readonly LibraryDataContext _context;
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public BooksController(LibraryDataContext context, MapperConfiguration config, IMapper mapper, ILookupBooks bookLookup, IBookCommands bookCommands)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _bookLookup = bookLookup;
            _bookCommands = bookCommands;
        }

        [HttpPut("books/{id:int}/genre")]
        public async Task<ActionResult> UpdateGenre(int id, [FromBody] string newGenre)
        {
            bool didUpdateGood = await _bookCommands.TryToUpdateGenreAsyncIfItsNotTooMuchToAsk(id, newGenre);
            if (didUpdateGood)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("books/{id:int}")]
        public async Task<ActionResult> RemoveBookFromInventory(int id)
        {
            await _bookCommands.Remove(id);
            return NoContent(); // 204 - it means "ok, but don't look for any data. I got nothing.
        }

        [HttpPost("books")]
        [ValidateModel]
        public async Task<ActionResult<GetBookDetailsResponse>> AddBook([FromBody] PostBookRequest bookToAdd)
        {
                GetBookDetailsResponse response = await _bookCommands.AddBook(bookToAdd);
                return CreatedAtRoute("books#getbookdetails", new { id = response.Id }, response);
            
        }


        /// <summary>
        /// This allows you to look up a book from our inventory by providing the ID of the book
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <returns>The book that matches that request, or a 404</returns>
        [HttpGet("books/{id:int}", Name = "books#getbookdetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBookDetailsResponse>> GetBookDetails(int id)
        {
            GetBookDetailsResponse response = await _bookLookup.GetBookById(id);

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("books")]
        [Produces("application/json")]
        public async Task<ActionResult<GetBooksResponse>> GetAllBooks()
        {
            GetBooksResponse response = await _bookLookup.GetAllBooks();
            return Ok(response);
        }

    }
}
