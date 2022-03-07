using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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

    public CompanyDto CreateCompany(CompanyCreateDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        _repository.Companies.CreateCompany(companyEntity);
        _repository.Save();

        return _mapper.Map<CompanyDto>(companyEntity);
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = _repository.Companies.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        return _mapper.Map<CompanyDto>(company);
    }
}