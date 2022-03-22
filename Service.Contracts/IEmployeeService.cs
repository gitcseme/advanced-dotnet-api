using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
	IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
	EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
	EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreateDto, bool trackChanges);
	void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
	void UpdateEmployeeForComapny(Guid companyId, Guid employeeId, EmployeeUpdateDto updateDto, bool cmpTrackChanges, bool empTrackChanges);
}