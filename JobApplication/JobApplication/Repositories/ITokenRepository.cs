using JobApplication.Models.Domain;

namespace JobApplication.Repositories
{
    public interface ITokenRepository
    {
        string createJWTToken(ApplicationUser user, List<string> roles);
    }
}
