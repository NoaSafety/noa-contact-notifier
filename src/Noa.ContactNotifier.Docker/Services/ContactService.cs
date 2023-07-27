using Noa.ContactNotifier.Contract.v1;
using Noa.ContactNotifier.Docker.Mappers;
using Noa.ContactNotifier.Repository.Repositories;

namespace Noa.ContactNotifier.Docker.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<List<Contact>> GetUserContactsAsync(Guid userId)
    {
        var contacts = await _contactRepository.GetUserContactsAsync(userId);
        return contacts.Select(c => c.ToContract()).ToList();
    }

    public async Task UpdateUserContactsAsync(Guid userId, List<Contact> contacts)
    {
        var modelContacts = contacts.Select(c => c.ToModel(userId)).ToList();
        await _contactRepository.UpdateUserContactsAsync(userId, modelContacts);
    }
}
