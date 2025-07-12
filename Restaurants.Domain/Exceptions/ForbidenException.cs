namespace Restaurants.Domain.Exceptions;

public class ForbidenException: Exception
{
    public ForbidenException(string message): base(message){}
}
