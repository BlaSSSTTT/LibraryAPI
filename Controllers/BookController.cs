using AutoMapper;
using LibrariAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public BookController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<BookViewModel>>(books));
    }

    // GET: api/book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookViewModel>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<BookViewModel>(book));
    }

    // POST: api/book
    [HttpPost]
    public async Task<ActionResult<BookViewModel>> PostBook(BookViewModel bookViewModel)
    {
        var book = _mapper.Map<Book>(bookViewModel);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var createdBookViewModel = _mapper.Map<BookViewModel>(book);
        return CreatedAtAction("GetBook", new { id = createdBookViewModel.Id }, createdBookViewModel);
    }

    // PUT: api/book/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, BookViewModel bookViewModel)
    {
        if (id != bookViewModel.Id)
        {
            return BadRequest();
        }

        var book = _mapper.Map<Book>(bookViewModel);
        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/book/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
