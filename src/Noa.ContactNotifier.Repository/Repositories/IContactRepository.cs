using Noa.ContactNotifier.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noa.ContactNotifier.Repository.Repositories;

public interface IContactRepository
{
    Task<List<Contact>> GetUserContactsAsync(Guid userId);

    Task UpdateUserContactsAsync(Guid userId, List<Contact> contacts);

}
