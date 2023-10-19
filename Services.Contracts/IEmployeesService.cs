using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges);
        public EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges);
    }
}