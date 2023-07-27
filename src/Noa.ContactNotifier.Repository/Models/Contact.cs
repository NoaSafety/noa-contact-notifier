using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noa.ContactNotifier.Repository.Models;

public class Contact
{
    [BsonId]
    public ObjectId ContactId { get; set; }
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Forename { get; set; }
    public string? PhoneNumber { get; set; }
}
