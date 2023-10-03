using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges);
    }
}