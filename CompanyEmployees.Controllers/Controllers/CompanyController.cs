using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompanyController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var compaines = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(compaines);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDTO company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDTO object is null.");

            var createdCompany = _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/{ids}", Name = "CompanyColellection")]
        public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, trackChanges: false);

            return Ok(companies);
        }
    }
}
