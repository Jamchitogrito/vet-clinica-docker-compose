namespace api.dto;

public class PetCreateDto
{
    public string Klichka { get; set; } = "";
    public string? Vid { get; set; }
    public string? Poroda { get; set; }
    public int Vozrast { get; set; }
    public int OwnerId { get; set; }
}