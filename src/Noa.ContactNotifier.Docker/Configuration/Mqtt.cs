namespace Noa.ContactNotifier.Docker.Configuration
{
    public class Mqtt
    {
        public string ClientId { get; set; }
        public string MqttBrokerAddress { get; set; }
        public int MqttBrokerPort { get; set; }
    }
}
