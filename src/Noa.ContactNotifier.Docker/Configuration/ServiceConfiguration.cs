namespace Noa.ContactNotifier.Docker.Configuration
{
    public class ServiceConfiguration
    {
        public const string ServiceName = "NoaContactNotifier";

        public Database Database { get; set; }

        public Api Api { get; set; }

        public Mqtt Mqtt { get; set; }

        public Twilio Twilio { get; set; }
    }
}
