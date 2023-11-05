using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeesFromDb = _repository.Employee.GetEmployees(companyId,
            trackChanges);

            return _mapper.Map<IEnumerable<EmployeeDTO>>(employeesFromDb); ;
        }

        public EmployeeDTO GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);

            if (employeeDb is null)
                throw new EmployeeNotFoundException(employeeId);

            return _mapper.Map<EmployeeDTO>(employeeDb);
        }

        public EmployeeDTO CreateEmployee(Guid companyId, EmployeeForCreationDTO employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            return _mapper.Map<EmployeeDTO>(employeeEntity);
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = _repository.Employee.GetEmployee(companyId, id, trackChanges);

            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(companyId);

            _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Save();
        }

        public void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);

            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
            _repository.Save();
        }

        public (EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);

            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDTO>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public void SaveChangeForPatch(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            _repository.Save();
        }
    }
}
