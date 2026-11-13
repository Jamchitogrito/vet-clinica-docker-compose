using System;
using System.Collections.Generic;

namespace api.models;

public partial class SpisokUslug
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Cena { get; set; }

    public int? Kolichesto { get; set; }

    public int? NaznId { get; set; }

    public virtual ICollection<Diagnoz> Diagnozs { get; set; } = new List<Diagnoz>();

    public virtual SpisokNaznacheni? Nazn { get; set; }
}
