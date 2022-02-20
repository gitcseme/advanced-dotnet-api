using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;

	public CompanyService(IRepositoryManager repository, ILoggerManager logger)
	{
		_repository = repository;
		_logger = logger;
	}

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        try
        {
            return _repository.Companies.GetAllCompanies(trackChanges);
        }
        catch (Exception ex)
        {
            _logger.LogError(String.Format("Error in {0} service method {1}", nameof(GetAllCompanies), ex));
            throw;
        }
    }
}