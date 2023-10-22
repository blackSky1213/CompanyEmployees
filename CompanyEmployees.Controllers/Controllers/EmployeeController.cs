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
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);

            return Ok(employees);
        }

        [HttpGet("{id:Guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDTO employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDTO object is null.");

            var employeeReturn = _service.EmployeeService.CreateEmployee(companyId, employee, false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeReturn.Id }, employeeReturn);
        }
    }
}
