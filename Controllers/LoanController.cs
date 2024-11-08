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
public class LoanController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;

    public LoanController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/loan
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanViewModel>>> GetLoans()
    {
        var loans = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Reader)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<LoanViewModel>>(loans));
    }

    // GET: api/loan/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanViewModel>> GetLoan(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.Reader)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (loan == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<LoanViewModel>(loan));
    }

    // POST: api/loan
    [HttpPost]
    public async Task<ActionResult<LoanViewModel>> PostLoan(LoanViewModel loanViewModel)
    {
        var loan = _mapper.Map<Loan>(loanViewModel);
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        var createdLoanViewModel = _mapper.Map<LoanViewModel>(loan);
        return CreatedAtAction("GetLoan", new { id = createdLoanViewModel.Id }, createdLoanViewModel);
    }

    // PUT: api/loan/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLoan(int id, LoanViewModel loanViewModel)
    {
        if (id != loanViewModel.Id)
        {
            return BadRequest();
        }

        var loan = _mapper.Map<Loan>(loanViewModel);
        _context.Entry(loan).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LoanExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/loan/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
        {
            return NotFound();
        }

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LoanExists(int id)
    {
        return _context.Loans.Any(e => e.Id == id);
    }
}
