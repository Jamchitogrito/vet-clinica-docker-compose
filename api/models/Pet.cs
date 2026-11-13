using System;
using System.Collections.Generic;

namespace api.models;

public partial class Pet
{
    public int Id { get; set; }

    public string Klichka { get; set; } = null!;

    public string? Vid { get; set; }

    public string? Poroda { get; set; }

    public int Vozrast { get; set; }

    public int OwnerId { get; set; }

    public virtual ICollection<Diagnoz> Diagnozs { get; set; } = new List<Diagnoz>();

    public virtual Owner Owner { get; set; } = null!;
}
