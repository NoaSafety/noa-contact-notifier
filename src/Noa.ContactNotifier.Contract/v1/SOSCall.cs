using System.Text.Json.Serialization;

namespace Noa.ContactNotifier.Contract.v1;

public class SOSCall
{
    [JsonPropertyName("id")]
    public Guid UserId { get; set; }

    [JsonPropertyName("beam")]
    public Guid Beam { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("time")]
    public UInt32 Timestamp { get; set; }
}