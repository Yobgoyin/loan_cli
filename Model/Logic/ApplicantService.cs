using applicant_api.Model.Data.Class;
using applicant_api.Model.Data.Helper;
using applicant_api.Model.Logic.Contracts;
using applicant_api.ServiceResponder;

namespace applicant_api.Model.Logic
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _repository;

        public ApplicantService(IApplicantRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Applicant>> GetApplicantById(int id)
        {
            ServiceResponse<Applicant> response = new();
            try
            {
                var _applicant = await _repository.GetApplicant(id);
                response.Success = true;
                response.Data = _applicant;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public async Task<ServiceResponse<Applicant>> GetApplicantByGuid(Guid guid)
        {

            ServiceResponse<Applicant> response = new();
            try
            {
                var _applicant = await _repository.GetApplicant(guid);
                //_applicant.Quote = await _repository.GetQuote(_applicant.QuoteId);
                
                response.Success = true;
                response.Data = _applicant;
                response.Message = "";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public async Task<ServiceResponse<Applicant>> CreateApplicantAsync(Applicant applicant)
        {
            BlackList bList = new BlackList();
            
            int applicantId = await _repository.CheckExistingApplication(applicant);
            ServiceResponse<Applicant> response = new();
            try
            {
                if (!isLegal(applicant.DOB))
                {
                    response.Success = false;
                    response.Data = null;
                    response.Message = "error";
                    response.ErrorMessages = new List<string> { "not of Legal Age; must be 18 and above" };
                    return response;
                }
                if (bList.MobileNumbers.IndexOf(applicant.Mobile) >=0 )
                {
                    response.Success = false;
                    response.Data = null;
                    response.Message = "error";
                    response.ErrorMessages = new List<string> { "not of Legal Age; must be 18 and above" };
                    return response;
                }

                if (applicantId == 0)
                {
                    applicant.GUID = Guid.NewGuid();
                    if (!await _repository.SaveApplicant(applicant))
                    {
                        response.Success = false;
                        response.Data = null;
                        response.Message = "error";
                        return response;
                    }
                    response.Success = true;
                    response.Data = applicant;
                    response.Message = "";
                }
                else
                {
                    response = await GetApplicantById(applicantId);
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;

        }
        public async Task<ServiceResponse<Applicant>> UpdateApplicantAsync(Guid guid, Applicant applicant)
        {
            Applicant existingApplicant = await _repository.GetApplicant(guid);
            //Quote existingQuote = await _repository.GetQuote(existingApplicant.QuoteId);
            //existingQuote.Amount = applicant.Quote.Amount;
            //existingQuote.Term = applicant.Quote.Term;
            ServiceResponse<Applicant> response = new();
            try
            {
                if (!isLegal(applicant.DOB))
                {
                    response.Success = false;
                    response.Data = null;
                    response.Message = "error";
                    response.ErrorMessages = new List<string> { "not of Legal Age; must be 18 and above" };
                    return response;
                }

                if (existingApplicant.Id == 0)
                {
                    response.Success = false;
                    response.Data = null;
                    response.Message = "error";
                    return response;
                }
                else
                {
                    existingApplicant.Salutation = applicant.Salutation;
                    existingApplicant.FirstName = applicant.FirstName;
                    existingApplicant.LastName = applicant.LastName;
                    existingApplicant.DOB = applicant.DOB;
                    existingApplicant.Mobile = applicant.Mobile;
                    existingApplicant.Email = applicant.Email;
                    //existingApplicant.Quote = existingQuote;
                    await _repository.UpdateApplicant(existingApplicant);
                    response.Success = true;
                    response.Data = applicant;
                    response.Message = "";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;

        }
        public Uri GenerateApplicationURL(Guid guid)
        {
            string url = String.Format("{0}/{1}", Constants.targetLocalHttp, guid);
            return new Uri(url);
        }

        public bool isLegal(DateTime dob)
        {
            bool isLegal = false;
            DateTime today = DateTime.Now;
            int year = today.Year - dob.Year;
            int month = today.Month - dob.Month;
            int day = today.Day - dob.Day;


            if (year == 17)
            {
                if(month ==0)
                {
                    if (day >= 0)
                    {
                        isLegal = true;
                    }
                    else
                    {
                        isLegal = false;
                    }
                }
                else if(month >0)
                {
                    isLegal = true;
                }
                else
                {
                    isLegal=false;
                }
            }
            else if(year > 17)
            {
                isLegal = true;
            }
            else
            {
                isLegal=false;
            }
            return isLegal;
        }

    }
}
