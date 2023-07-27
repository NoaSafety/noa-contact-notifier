using Microsoft.AspNetCore.Mvc;
using Noa.ContactNotifier.Contract.v1;

namespace Noa.ContactNotifier.Docker.Services;

public interface IContactService
{
    Task<List<Contact>> GetUserContactsAsync(Guid userId);

    Task UpdateUserContactsAsync(Guid userId, List<Contact> contacts);

}
