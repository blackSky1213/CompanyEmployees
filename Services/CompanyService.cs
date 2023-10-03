using AutoMapper;
using Contracts;
using Entities.Models;
using Services.Contracts;
using Shared.DataTransferObjects;

namespace Services
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDTO>  GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                return _mapper.Map<IEnumerable<CompanyDTO>>(companies);

            } catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} {ex}");
                throw;
            }
        }
    }
}