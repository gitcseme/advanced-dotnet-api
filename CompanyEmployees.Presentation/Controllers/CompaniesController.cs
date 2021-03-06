using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public IActionResult GetCompany(Guid id)
    {
        var company = _service.CompanyService.GetCompany(id, trackChanges: false);
        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyCreateDto company)
    {
        if (company is null)
            return BadRequest("CompanyCreateDto object is null");

        var createdCompany = _service.CompanyService.CreateCompany(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var companies = _service.CompanyService.GetByIds(ids, false);
        return Ok(companies);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyCreateDto> companyCollection)
    {
        var result = _service.CompanyService.CreateCompanyCollection(companyCollection);
        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
    }

    [HttpDelete("{companyId:guid}")]
    public IActionResult DeleteCompany(Guid companyId)
    {
        _service.CompanyService.DeleteCompany(companyId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{companyId:guid}")]
    public IActionResult UpdateCompany(Guid companyId, [FromBody] CompanyUpdateDto companyDto)
    {
        if (companyDto is null)
            return BadRequest("CompanyUpdateDto is null");

        _service.CompanyService.UpdateCompany(companyId, companyDto, trackChanges: true);
        
        return NoContent(); // 204
    }

}
