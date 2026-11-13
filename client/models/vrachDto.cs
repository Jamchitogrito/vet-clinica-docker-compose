using System;
using System.Collections.Generic;

namespace client.dto;

public partial class VrachDto
{
    public int Id { get; set; }

    public string? Fio { get; set; }

    public string? Spec { get; set; }

    public string? Telefon { get; set; }

    public string? Login { get; set; }
}
