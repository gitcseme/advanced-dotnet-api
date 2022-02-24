namespace Entities.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message)
    { }
}

public class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid companyId)
        :base(string.Format("company with id: {0} doesn't exists", companyId))
    {

    }
}