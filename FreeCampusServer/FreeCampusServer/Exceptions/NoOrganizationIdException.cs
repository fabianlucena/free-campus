using RFHttpExceptions.Exceptions;

namespace FreeCampusServer.Exceptions
{
    public class NoOrganizationIdException()
        : HttpException(400, "OrganizationId is missing.")
    {
    }
}