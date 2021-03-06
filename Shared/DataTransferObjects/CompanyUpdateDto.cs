namespace Shared.DataTransferObjects;

public record CompanyUpdateDto(string Name, string Address, string Country, IEnumerable<EmployeeCreateDto> Employees);
