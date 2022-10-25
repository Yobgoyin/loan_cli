using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using applicant_api.Context;
using applicant_api.Model.Data.Class;
using applicant_api.Model.Data.Helper;
using applicant_api.Model.Logic;
using applicant_api.Model.Logic.Contracts;
using System.Net;

namespace applicant_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        
        private readonly IApplicantService _applicantService;
        public ApplicantsController(IApplicantService applicantService)
        {
            this._applicantService = applicantService;
        }


        [HttpGet("{guid:Guid}", Name = "GetApplicantByGUID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Applicant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Applicant>> GetByGUID(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return BadRequest(guid);
            }
            var applicant = await _applicantService.GetApplicantByGuid(guid);

            return Ok(applicant);
        }
        [HttpGet(Name = "ReturnUrl")]
        public HttpResponseMessage ReturnUrl(Guid guid)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Redirect);
            response.Headers.Location = new Uri("https://www.google.com");
            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Applicant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            if (applicant == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var _newApplicant = await _applicantService.CreateApplicantAsync(applicant);
            if(_newApplicant.Success == false)
            {
                return BadRequest();
            }
            else
            {
                return Created(_applicantService.GenerateApplicationURL(_newApplicant.Data.GUID), new { guid = _newApplicant });
            }

        }


        [HttpPut("{guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Applicant>> UpdateApplicant(Guid guid, Applicant applicant)
        {
            if (applicant == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _applicant = await _applicantService.UpdateApplicantAsync(guid, applicant);
            return Ok(_applicant);
        }
        #region Backup

        //private readonly ApplicationQuoteContext _context;
        //public ApplicantsController(ApplicationQuoteContext context)
        //{
        //    _context = context;
        //}
        //public ApplicantsController (IApplicantService service, ApplicationQuoteContext context)
        //{
        //    _service = service;
        //     _context = context;
        //}

        //// GET: api/Applicants
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicant()
        //{
        //    return await _context.Applicant.ToListAsync();
        //}

        //// GET: api/Applicants/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Applicant>> GetApplicant(int id)
        //{
        //    var applicant = await _context.Applicant.FindAsync(id);

        //    if (applicant == null)
        //    {
        //        return NotFound();
        //    }

        //    return applicant;
        //}

        //// PUT: api/Applicants/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutApplicant(int id, Applicant applicant)
        //{
        //    if (id != applicant.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(applicant).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicantExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Applicants
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        //{
        //    _context.Applicant.Add(applicant);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetApplicant", new { id = applicant.Id }, applicant);
        //}

        //// DELETE: api/Applicants/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteApplicant(int id)
        //{
        //    var applicant = await _context.Applicant.FindAsync(id);
        //    if (applicant == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Applicant.Remove(applicant);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ApplicantExists(int id)
        //{
        //    return _context.Applicant.Any(e => e.Id == id);
        //}
        #endregion
    }
}
