using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using Noa.ContactNotifier.Contract.v1;
using Noa.ContactNotifier.Docker.Configuration;
using Noa.ContactNotifier.Docker.Services;

namespace Noa.PocketUi.Main.Services;

public class MqttService : IMqttService
{
    private readonly INotificationService _notificationService;
    private readonly IMqttClient _mqttClient;
    private readonly ServiceConfiguration _config;
    public Task Initialization { get; private set; }

    public MqttService(INotificationService notificationService, ServiceConfiguration config)
    {
        _notificationService = notificationService;
        _config = config;
        _mqttClient = new MqttFactory().CreateMqttClient();
        Initialization = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        SetupSOSHandler();

        await _mqttClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId(_config.Mqtt.ClientId)
            .WithTcpServer(_config.Mqtt.MqttBrokerAddress, _config.Mqtt.MqttBrokerPort)
            .WithCleanSession()
            .Build());

        await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptions()
        {
            TopicFilters = new() { new() { Topic = "noa/#" } }
        });
    }

    private void SetupSOSHandler()
    {
        _mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var sosCall = JsonSerializer.Deserialize<SOSCall>(e.ApplicationMessage.ConvertPayloadToString());
            _notificationService.HandleSOSAsync(sosCall);
            return Task.CompletedTask;
        };
    }
}
