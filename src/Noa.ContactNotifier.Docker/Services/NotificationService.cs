using Microsoft.Extensions.Caching.Memory;
using Noa.ContactNotifier.Contract.v1;
using Noa.ContactNotifier.Docker.Configuration;
using Noa.ContactNotifier.Docker.Utils;
using Noa.ContactNotifier.Repository.Models;
using Noa.ContactNotifier.Repository.Repositories;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Contact = Noa.ContactNotifier.Repository.Models.Contact;

namespace Noa.ContactNotifier.Docker.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IContactRepository _contactRepository;
        private readonly ServiceConfiguration _config;
        private readonly DictionnaryCache<Guid, SOSCall> _sosCache = new DictionnaryCache<Guid, SOSCall>();
        public NotificationService(IAuthenticationService authenticationService, IContactRepository contactRepository, ServiceConfiguration config)
        {
            _authenticationService = authenticationService;
            _contactRepository = contactRepository;
            _config = config;
            TwilioClient.Init(config.Twilio.AccountSid, config.Twilio.AuthToken);
        }

        public async Task HandleSOSAsync(SOSCall sosCall)
        {
            if(_sosCache.Get(sosCall.UserId) == null) {
                var contacts = await _contactRepository.GetUserContactsAsync(sosCall.UserId);
                var callerData = await _authenticationService.GetPublicUserData(sosCall.UserId);
                foreach (var contact in contacts)
                {
                    sendTwilioSosSms(contact, sosCall, callerData);
                }
                _sosCache.Store(sosCall.UserId, sosCall, TimeSpan.FromSeconds(_config.Api.SosBufferDelay));
            }
        }

        private void sendTwilioSosSms(Contact contact, SOSCall sosCall, UserData userData)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(contact.PhoneNumber));
            messageOptions.MessagingServiceSid = _config.Twilio.MessagingServiceSid;
            messageOptions.Body = $"Hello {contact.Name}, your NOA contact {userData.UserName} is in danger!\n\nhttp://maps.google.com/?q={sosCall.Latitude},{sosCall.Longitude}\r\n";
            MessageResource.Create(messageOptions);
        }
    }
}
