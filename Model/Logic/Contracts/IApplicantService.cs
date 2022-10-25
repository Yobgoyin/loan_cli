using applicant_api.Model.Data.Class;
using applicant_api.ServiceResponder;

namespace applicant_api.Model.Logic.Contracts
{
    public interface IApplicantService
    {
        Task<ServiceResponse<Applicant>> CreateApplicantAsync(Applicant applicant);
        Task<ServiceResponse<Applicant>> UpdateApplicantAsync(Guid guid, Applicant applicant);
        Task<ServiceResponse<Applicant>> GetApplicantById(int id);
        Task<ServiceResponse<Applicant>> GetApplicantByGuid(Guid id);
        Uri GenerateApplicationURL(Guid guid);
    }
}