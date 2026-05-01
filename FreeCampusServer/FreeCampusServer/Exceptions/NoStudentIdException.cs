using RFHttpExceptions.Exceptions;

namespace FreeCampusServer.Exceptions
{
    public class NoStudentIdException()
        : HttpException(400, "StudentId is missing.")
    {
    }
}