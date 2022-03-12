using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
	{
	}

    public void CreateCompany(Company company) => Create(company);

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        return FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
    }

    public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        return Find(c => ids.Contains(c.Id), trackChanges)
            .ToList();
    }

    public Company? GetCompany(Guid companyId, bool trackChanges)
    {
        var company = Find(c => c.Id.Equals(companyId), trackChanges)
            .SingleOrDefault();

        return company;
    }
}