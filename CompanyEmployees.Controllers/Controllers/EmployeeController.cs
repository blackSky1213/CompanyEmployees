using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeeController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
        {
            var employees = await _service.EmployeeService.GetEmployeesAsync(companyId, trackChanges: false);

            return Ok(employees);
        }

        [HttpGet("{id:Guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee =await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDTO employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDTO object is null.");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var employeeReturn =await _service.EmployeeService.CreateEmployeeAsync(companyId, employee, false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeReturn.Id }, employeeReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
           await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDTO employeeForUpdate)
        {
            if (employeeForUpdate is null)
                return BadRequest("EmployeeForUpdateDTO ojbect is null.");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

           await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employeeForUpdate, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDTO> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result =await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, compTrackChagnes: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.employeeToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            TryValidateModel(result.employeeToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.EmployeeService.SaveChangeForPatchAsync(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }
    }
}
