using System.Text.Json.Serialization;

namespace Noa.ContactNotifier.Contract.v1;
public class UserData
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("username")]
    public string UserName { get; set; }
}
