using AutoMapper;
using Contracts;
using Entities.Exceptions;
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

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
        }

        public CompanyDTO GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            return _mapper.Map<CompanyDTO>(company);
        }

        public CompanyDTO CreateCompany(CompanyForCreationDTO company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();

            return _mapper.Map<CompanyDTO>(companyEntity);
        }

        public IEnumerable<CompanyDTO> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var companyEntities = _repository.Company.GetByIds(ids, trackChanges);

            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();

            return _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
        }
    }
}