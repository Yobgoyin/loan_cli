using applicant_api.Context;
using applicant_api.Model.Data.Class;
using applicant_api.Model.Data.Helper;
using applicant_api.Model.Logic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace applicant_api.Model.Logic
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicationQuoteContext _context;
        public ApplicantRepository(ApplicationQuoteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Applicant>> ListApplicants()
        {
            return await _context.Applicant.ToListAsync();
        }

        public async Task<Applicant> GetApplicant(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);

            if (applicant == null)
            {
                throw new Exception("No Records Found");
            }

            return applicant;
        }
        public async Task<Applicant> GetApplicant(Guid id)
        {
            var applicant = await _context.Applicant.FirstOrDefaultAsync(app => app.GUID == id);

            if (applicant == null)
            {
                throw new Exception("No Records Found");
            }

            return applicant;
        }


        public async Task<bool> SaveApplicant(Applicant applicant)
        {
            _context.Applicant.Add(applicant);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<int> CheckExistingApplication(Applicant applicant)
        {
            return await _context.Applicant
                .Where(
                app =>
                    app.FirstName == applicant.FirstName &&
                    app.LastName == applicant.LastName &&
                    app.DOB == applicant.DOB)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateApplicant(Applicant applicant)
        {
            _context.Entry(applicant).State = EntityState.Modified;
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task SaveQuotes(Quote quote)
        {
            quote.GUID = Guid.NewGuid();
            await _context.Quote.AddAsync(quote);
        }
        public async Task<Quote> GetQuote(int id)
        {
            var quote = await _context.Quote.FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null)
            {
                throw new Exception("No Records Found");
            }
            return quote;
        }
    }
}
