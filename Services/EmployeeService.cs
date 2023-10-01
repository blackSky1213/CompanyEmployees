using Contracts;
using Services.Contracts;

namespace Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public EmployeeService(IRepositoryManager repository, ILoggerManager loggerManager)
        {
            _repository = repository;
            _loggerManager = loggerManager;
        }
    }
}
