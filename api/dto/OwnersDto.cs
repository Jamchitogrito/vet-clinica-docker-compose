using api.models;

namespace api.dto;

public class OwnerDto
{
    public int Id { get; set; }
    public string Fio { get; set; } = "";
    public string Telefon { get; set; } = "";
    public string? Adres { get; set; }
    public int PetsCount { get; set; }
    public OwnerDto() { }
    public OwnerDto(Owner o)
    {
        Id = o.Id;
        Fio = o.Fio;
        Telefon = o.Telefon;
        Adres = o.Adres;
        PetsCount = o.Pets?.Count ?? 0;
    }
}