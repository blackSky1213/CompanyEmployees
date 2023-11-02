using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges);
        EmployeeDTO CreateEmployee(Guid companyId, EmployeeForCreationDTO employeeForCreation, bool trackChanges);
        void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges);
        void UpdateEmployeeForCompany(Guid companyId, Guid id,
            EmployeeForUpdateDTO employeeForUpdate,
            bool compTrackChanges, bool empTrackChanges);
    }
}