using System;
using System.Collections.Generic;
using api.models;

namespace api.dto;

public partial class VrachDto
{
    public int Id { get; set; }

    public string? Fio { get; set; }

    public string? Spec { get; set; }

    public string? Telefon { get; set; }

    public string? Login { get; set; }
    public VrachDto(Vrach vrach)
    {
        Id = vrach.Id;
        Fio = vrach.Fio;
        Spec = vrach.Spec;
        Telefon = vrach.Telefon;
        Login = vrach.Login;
    }
}
