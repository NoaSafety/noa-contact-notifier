using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Noa.ContactNotifier.Repository.Models;

namespace Noa.ContactNotifier.Repository.Repositories;

public class MgdbContactRepository : IContactRepository
{
    private readonly IMongoCollection<Contact> _contactCollection;

    public MgdbContactRepository(IMongoDatabase database)
    {
        _contactCollection = database.GetCollection<Contact>("contacts");
    }

    public async Task<List<Contact>> GetUserContactsAsync(Guid userId)
    {
        var contacts = await _contactCollection.Find(c => c.UserId == userId).ToListAsync();
        return contacts;
    }

    public async Task UpdateUserContactsAsync(Guid userId, List<Contact> contacts)
    {
        await _contactCollection.DeleteManyAsync(c => c.UserId == userId);
        await _contactCollection.InsertManyAsync(contacts);
    }
}
