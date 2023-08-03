using Microsoft.IdentityModel.Tokens;

namespace TechChallengeBlogWebApi.Interfaces
{
    public interface IRsaSecurityKeyFactory
    {
        RsaSecurityKey Create();
    }
}