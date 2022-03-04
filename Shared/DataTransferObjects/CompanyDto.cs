namespace Shared.DataTransferObjects;

//[Serializable]
public record CompanyDto
{  //(Guid Id, string Name, string FullAddress);
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? FullAddress { get; init; }
}
