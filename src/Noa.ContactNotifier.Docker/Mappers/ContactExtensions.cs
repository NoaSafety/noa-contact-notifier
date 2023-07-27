using Noa.ContactNotifier.Repository.Models;

namespace Noa.ContactNotifier.Docker.Mappers;

public static class ContactExtensions
{
    // TODO: fix conversion bewteen id types
    public static Contract.v1.Contact ToContract(this Contact contact) => new()
    {
        Name = contact.Name,
        //Forename = contact.Forename,
        PhoneNumber = contact.PhoneNumber,
    };

    public static Contact ToModel(this Contract.v1.Contact contact, Guid userId) => new()
    {
        UserId = userId,
        ContactId = MongoDB.Bson.ObjectId.GenerateNewId(),
        Name = contact.Name,
        //Forename = contact.Forename,
        PhoneNumber = contact.PhoneNumber,
    };
}
