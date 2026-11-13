using System;
using System.Collections.Generic;

namespace api.models;

public partial class SpisokNaznacheni
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SpisokUslug> SpisokUslugs { get; set; } = new List<SpisokUslug>();
}
