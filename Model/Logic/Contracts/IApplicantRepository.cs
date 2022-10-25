using applicant_api.Model.Data.Class;

namespace applicant_api.Model.Logic.Contracts
{
    public interface IApplicantRepository
    {
        Task<int> CheckExistingApplication(Applicant applicant);
        Task<Applicant> GetApplicant(Guid id);
        Task<Applicant> GetApplicant(int id);
        Task<IEnumerable<Applicant>> ListApplicants();
        Task<bool> SaveApplicant(Applicant applicant);
        Task<bool> UpdateApplicant(Applicant applicant);
        Task SaveQuotes(Quote quote);
        Task<Quote> GetQuote(int id);
    }
}