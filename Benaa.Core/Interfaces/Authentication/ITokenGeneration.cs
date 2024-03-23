using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.Authentication
{
    public interface ITokenGeneration
    {
        string GenerateTokenString(User user, IList<string> userRoles);
    }
}