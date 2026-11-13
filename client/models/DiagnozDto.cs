using System.Text.Json.Serialization;

namespace client.dto;

public class DiagnozDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("stadia")] public string? Stadia { get; set; }
    [JsonPropertyName("vrachId")] public int? VrachId { get; set; }
    [JsonPropertyName("uslugaId")] public int? UslugaId { get; set; }
    [JsonPropertyName("petId")] public int? PetId { get; set; }
    [JsonPropertyName("vrachFio")] public string? VrachFio { get; set; }
    [JsonPropertyName("uslugaName")] public string? UslugaName { get; set; }
    [JsonPropertyName("petKlichka")] public string? PetKlichka { get; set; }

    public string Display =>
        $"--{Id}  {Name ?? "хз"} / {Stadia ?? "хз"} / врач: {VrachFio ?? "?"} / питомец: {PetKlichka ?? "?"}";
}