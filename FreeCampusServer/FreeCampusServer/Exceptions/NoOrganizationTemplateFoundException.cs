using RFHttpExceptions.Exceptions;

namespace FreeCampusServer.Exceptions
{
    public class NoOrganizationTemplateFoundException()
        : HttpException(400, "No organization template found.");
}