namespace Restaurants.Domain.Exceptions;

public class ForbidenException(string userIdentifier,string resourceOperation)
    : Exception($"This user :{userIdentifier} is not authorize to perform the [{resourceOperation}] operation")
{
}
