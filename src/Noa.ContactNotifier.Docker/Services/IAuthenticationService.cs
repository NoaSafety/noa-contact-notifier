using Noa.ContactNotifier.Contract.v1;

namespace Noa.ContactNotifier.Docker.Services
{
    public interface IAuthenticationService
    {
        Task<UserData> GetPublicUserData(Guid userId);
    }
}
