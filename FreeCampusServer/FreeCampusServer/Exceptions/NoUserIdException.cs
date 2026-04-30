using RFHttpExceptions.Exceptions;

namespace FreeCampusServer.Exceptions
{
    public class NoUserIdException()
        : HttpException(400, "UserId is missing.")
    {
    }
}