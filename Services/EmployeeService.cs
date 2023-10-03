using AutoMapper;
using Contracts;
using Services.Contracts;

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
    }
}
