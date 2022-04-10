using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;

    public EmployeesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);
        return Ok(employees);
    }

    [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid employeeId)
    {
        var employee = _service.EmployeeService.GetEmployee(companyId, employeeId, trackChanges: false);
        return Ok(employee); // 200
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeCreateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeCreateDto is null");

        var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, employeeId = employeeToReturn.Id }, employeeToReturn); // 201
    }

    [HttpDelete("{employeeId:guid}")]
    public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
    {
        _service.EmployeeService.DeleteEmployeeForCompany(companyId, employeeId, false);
        return NoContent(); // 204
    }

    [HttpPut("{employeeId:guid}")]
    public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeUpdateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeUpdateDto is null");

        _service.EmployeeService.UpdateEmployeeForComapny(companyId, employeeId, employee, false, true);

        return NoContent(); // 204
    }

    [HttpPatch("{employeeId:guid}")]
    public IActionResult PartiallyUpdateEmployee(Guid companyId, Guid employeeId, 
        [FromBody] JsonPatchDocument<EmployeeUpdateDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("patchDoc sent from client is null");

        var result = _service.EmployeeService.GetEmployeeForPatch(companyId, employeeId, false, true);

        patchDoc.ApplyTo(result.employeeToPatch);

        _service.EmployeeService.SaveChangesForPatch(result.employeeToPatch, result.employeeEntity);
        return NoContent(); // 204
    }
}

