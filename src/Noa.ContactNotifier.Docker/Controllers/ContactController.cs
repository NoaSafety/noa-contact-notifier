using Microsoft.AspNetCore.Mvc;
using Noa.ContactNotifier.Contract.v1;
using Noa.ContactNotifier.Docker.Services;


namespace Noa.ContactNotifier.Docker.Controllers
{
    [ApiController]
    [Route("contact")]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpGet("{userId}")]
        public async Task<List<Contact>> GetUserContactsAsync([FromRoute] Guid userId)
        {
            var contacts = await _contactService.GetUserContactsAsync(userId);
              return contacts;
        }

        [HttpPut("{userId}")]
        public async Task UpdateUserContactsAsync([FromRoute] Guid userId, [FromBody] List<Contact> contacts)
        {
            await _contactService.UpdateUserContactsAsync(userId, contacts);
        }
    }
}