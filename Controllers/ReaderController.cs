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
public class ReaderController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public ReaderController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/reader
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReaderViewModel>>> GetReaders()
    {
        var readers = await _context.Readers.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ReaderViewModel>>(readers));
    }

    // GET: api/reader/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReaderViewModel>> GetReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);

        if (reader == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ReaderViewModel>(reader));
    }

    // POST: api/reader
    [HttpPost]
    public async Task<ActionResult<ReaderViewModel>> PostReader(ReaderViewModel readerViewModel)
    {
        var reader = _mapper.Map<Reader>(readerViewModel);
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        var createdReaderViewModel = _mapper.Map<ReaderViewModel>(reader);
        return CreatedAtAction("GetReader", new { id = createdReaderViewModel.Id }, createdReaderViewModel);
    }

    // PUT: api/reader/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, ReaderViewModel readerViewModel)
    {
        if (id != readerViewModel.Id)
        {
            return BadRequest();
        }

        var reader = _mapper.Map<Reader>(readerViewModel);
        _context.Entry(reader).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReaderExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/reader/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }

        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReaderExists(int id)
    {
        return _context.Readers.Any(e => e.Id == id);
    }
}
