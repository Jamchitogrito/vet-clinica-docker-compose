using api.models;

namespace api.dto;

public class DiagnozDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Stadia { get; set; }
    public int? VrachId { get; set; }
    public int? UslugaId { get; set; }
    public int? PetId { get; set; }
    public string? VrachFio { get; set; }
    public string? UslugaName { get; set; }
    public string? PetKlichka { get; set; }

    public DiagnozDto() { }

    public DiagnozDto(Diagnoz d)
    {
        Id = d.Id;
        Name = d.Name;
        Stadia = d.Stadia;
        VrachId = d.VrachId;
        UslugaId = d.UslugaId;
        PetId = d.PetId;
        VrachFio = d.Vrach?.Fio;
        UslugaName = d.Usluga?.Name;
        PetKlichka = d.Pet?.Klichka;
    }
}