using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

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

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        try
        {
            var companies = _repository.Companies.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Error in {0} service method {1}", nameof(GetAllCompanies), ex));
            throw;
        }
    }
}