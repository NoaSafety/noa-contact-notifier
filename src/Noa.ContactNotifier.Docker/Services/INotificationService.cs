using Noa.ContactNotifier.Contract.v1;

namespace Noa.ContactNotifier.Docker.Services
{
    public interface INotificationService
    {
        Task HandleSOSAsync(SOSCall sosCall);
    }
}
