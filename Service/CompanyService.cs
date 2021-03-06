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

    public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyCreateDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();

        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach (var company in companyEntities)
        {
            _repository.Companies.CreateCompany(company);
        }

        _repository.Save();

        var companyCollectionTopReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionTopReturn.Select(c => c.Id));

        return (companyCollectionTopReturn, ids);
    }

    public void DeleteCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        _repository.Companies.DeleteCompany(company);
        _repository.Save();
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = _repository.Companies.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var companyEntities = _repository.Companies.GetByIds(ids, trackChanges);
        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();

        return _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        return _mapper.Map<CompanyDto>(company);
    }

    public void UpdateCompany(Guid companyId, CompanyUpdateDto updateDto, bool trackChanges)
    {
        var companyEntity = _repository.Companies.GetCompany(companyId, trackChanges);
        if (companyEntity is null)
            throw new CompanyNotFoundException(companyId);

        _mapper.Map(updateDto, companyEntity);
        _repository.Save();
    }
}