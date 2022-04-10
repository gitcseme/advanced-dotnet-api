using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
	IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
	EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
	EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreateDto, bool trackChanges);
	void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
	void UpdateEmployeeForComapny(Guid companyId, Guid employeeId, EmployeeUpdateDto updateDto, bool cmpTrackChanges, bool empTrackChanges);
	(EmployeeUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid employeeId, bool cmpTrackChanges, bool empTrackChanges);
	void SaveChangesForPatch(EmployeeUpdateDto employeeToPatch, Employee employeeEntity);
}