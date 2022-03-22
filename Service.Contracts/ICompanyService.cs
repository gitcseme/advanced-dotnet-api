using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
	IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
	IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
	CompanyDto GetCompany(Guid companyId, bool trackChanges);
	
	CompanyDto CreateCompany(CompanyCreateDto company);
	(IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyCreateDto> companyCollection);
	void DeleteCompany(Guid companyId, bool trackChanges);
}