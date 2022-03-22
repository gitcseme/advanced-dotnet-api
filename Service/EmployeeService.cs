using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class EmployeeService : IEmployeeService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreateDto, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employeeEntity = _mapper.Map<Employee>(employeeCreateDto);
        _repository.Employees.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();

        return _mapper.Map<EmployeeDto>(employeeEntity);
    }

    public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employee = _repository.Employees.GetEmployee(companyId, employeeId, trackChanges);
        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        _repository.Employees.DeleteEmployee(employee);
        _repository.Save();
    }

    public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employee = _repository.Employees.GetEmployee(companyId, employeeId, trackChanges);
        if (employee is null)
            throw new EmployeeNotFoundException(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employees = _repository.Employees.GetEmployees(companyId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return employeesDto;
    }

    public void UpdateEmployeeForComapny(Guid companyId, Guid employeeId, EmployeeUpdateDto updateDto, 
        bool cmpTrackChanges, bool empTrackChanges)
    {
        var company = _repository.Companies.GetCompany(companyId, cmpTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);

        var employeeEntity = _repository.Employees.GetEmployee(companyId, employeeId, empTrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(employeeId);

        _mapper.Map(updateDto, employeeEntity);
        _repository.Save();
    }
}