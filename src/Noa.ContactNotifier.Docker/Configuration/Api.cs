namespace Noa.ContactNotifier.Docker.Configuration
{
    public class Api
    {
        public int Port { get; set; }
        public string CorsDomain { get; set; }

        public string AuthenticationServiceURL { get; set; }

        public int SosBufferDelay { get; set; }

    }
}
