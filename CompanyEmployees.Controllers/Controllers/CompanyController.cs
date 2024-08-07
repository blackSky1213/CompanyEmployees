﻿using CompanyEmployees.Presentation.ActionFilter;
using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompanyController(IServiceManager service) => _service = service;

        [HttpGet(Name ="GetCompanies")]
        [Authorize]
        public async Task<IActionResult> GetCompanies()
        {
            var compaines = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            return Ok(compaines);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        [ResponseCache(Duration = 60)]  
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost(Name ="CreateCompany")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDTO company)
        {
            var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/{ids}", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDTO> companyCollection)
        {
            var (companies, ids) = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);

            return CreatedAtRoute("CompanyCollection", new { ids }, companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.CompanyService.DeleteCompanyAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, CompanyForUpdateDTO company)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCompniesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTION");

            return Ok();
        }
    }
}
