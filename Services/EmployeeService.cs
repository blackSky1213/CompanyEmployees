using AutoMapper;
using Contracts;
using Entities.Exceptions;
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

        public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employeesFromDb = _repository.Employee.GetEmployees(companyId,
            trackChanges);

            return _mapper.Map<IEnumerable<EmployeeDTO>>(employeesFromDb); ;
        }

        public EmployeeDTO GetEmployee(Guid companyId,Guid employeeId,bool trackChanges) 
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);

            if (employeeDb is null)
                throw new EmployeeNotFoundException(employeeId);

           return _mapper.Map<EmployeeDTO>(employeeDb);
        }
    }
}
