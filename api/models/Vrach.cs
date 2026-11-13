using System;
using System.Collections.Generic;

namespace api.models;

public partial class Vrach
{
    public int Id { get; set; }

    public string? Fio { get; set; }

    public string? Spec { get; set; }

    public string? Telefon { get; set; }

    public string? Login { get; set; }

    public virtual ICollection<Diagnoz> Diagnozs { get; set; } = new List<Diagnoz>();
}
