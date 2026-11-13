using System;
using System.Collections.Generic;
using api.models;
namespace api.dto;

public partial class PetDto
{
    public int Id { get; set; }

    public string Klichka { get; set; } = null!;

    public string? Vid { get; set; }

    public string? Poroda { get; set; }

    public int Vozrast { get; set; }

    public int OwnerId { get; set; }

    public PetDto(){}
    public PetDto(Pet p)
    {
        Id = p.Id;
        Klichka = p.Klichka;
        Vid = p.Vid;
        Poroda = p.Poroda;
        Vozrast = p.Vozrast;
        OwnerId = p.OwnerId;
    }
}
