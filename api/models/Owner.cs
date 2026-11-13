using System;
using System.Collections.Generic;

namespace api.models;

public partial class Owner
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string? Adres { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
