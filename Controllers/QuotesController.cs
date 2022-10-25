using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using applicant_api.Context;
using applicant_api.Model.Data.Class;

namespace applicant_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly ApplicationQuoteContext _context;

        public QuotesController(ApplicationQuoteContext context)
        {
            _context = context;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuote()
        {
            return await _context.Quote.ToListAsync();
        }

        // GET: api/Quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quote.FirstOrDefaultAsync(x => x.ApplicantId == id);

            if (quote == null)
            {
                return NotFound();
            }
            quote.PMT = Math.Round(ComputePMT(4, Convert.ToDouble(quote.Term), Convert.ToDouble(quote.Amount)), 2);

            return quote;
        }

        // PUT: api/Quotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            if (QuoteExists(quote.ApplicantId))
            {
                var existingQuote = await _context.Quote.FirstOrDefaultAsync(x => x.ApplicantId == quote.ApplicantId);
                if (existingQuote != null)
                {
                    _context.Quote.Remove(existingQuote);
                }
            }
            _context.Quote.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.Quote.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quote.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuoteExists(int id)
        {
            return _context.Quote.Any(e => e.ApplicantId == id);
        }

        private double ComputePMT(double interest, double termLength, double amount)
        {
            double rate = (interest / 100);
            double computed = ((amount * ((rate) / 12)) / (1 - (Math.Pow((1 + ((rate) / 12)), (-1 * termLength)))));

            return computed;
        }
    }
}

