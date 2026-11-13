using System;
using System.Collections.Generic;

namespace api.models;

public partial class Diagnoz
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Stadia { get; set; }

    public int? VrachId { get; set; }

    public int? UslugaId { get; set; }

    public int? PetId { get; set; }

    public virtual Pet? Pet { get; set; }

    public virtual SpisokUslug? Usluga { get; set; }

    public virtual Vrach? Vrach { get; set; }
}
