using System.Text.Json.Serialization;

namespace client.dto;

public class PetDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("klichka")]
    public string? Klichka { get; set; }
    [JsonPropertyName("vid")]
    public string? Vid { get; set; }
    [JsonPropertyName("poroda")]
    public string? Poroda { get; set; }
    [JsonPropertyName("vozrast")]
    public int Vozrast { get; set; }
    [JsonPropertyName("ownerId")]
    public int OwnerId { get; set; }
    [JsonPropertyName("ownerFio")]
    public string? OwnerFio { get; set; }

    public string Display =>
        $"--{Id}  {Klichka ?? "хз"} / {Vid ?? "?"} / {Poroda ?? "?"} / {Vozrast} лет / хозяин: {OwnerFio ?? "?"}";
}