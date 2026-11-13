using System.Text.Json.Serialization;

namespace client.dto;

public class OwnerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("fio")]
    public string? Fio { get; set; }
    [JsonPropertyName("telefon")]
    public string? Telefon { get; set; }
    [JsonPropertyName("adres")]
    public string? Adres { get; set; }
    [JsonPropertyName("petsCount")]
    public int PetsCount { get; set; }

    public string Display =>
        $"--{Id}  {Fio ?? "хз"} / тел: {Telefon ?? "?"} / адрес: {Adres ?? "?"} / питомцев: {PetsCount}";
}