using Microsoft.AspNetCore.Mvc;
using api.dto;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/diagnozs")]
public class diagnozController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<DiagnozDto>> GetAll()
    {
        using var context = new KlinikaContext();
        var entities = context.Diagnozs
            .Include(d => d.Vrach)
            .Include(d => d.Usluga)
            .Include(d => d.Pet)
            .ToList();

        var dtos = entities.Select(d => new DiagnozDto(d)).ToList();
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public ActionResult<DiagnozDto> GetById(int id)
    {
        using var context = new KlinikaContext();
        var diagnoz = context.Diagnozs
            .Include(d => d.Vrach)
            .Include(d => d.Usluga)
            .Include(d => d.Pet)
            .FirstOrDefault(d => d.Id == id);

        if (diagnoz == null) return NotFound();
        return Ok(new DiagnozDto(diagnoz));
    }

    [HttpPost]
    public ActionResult<DiagnozDto> Create([FromBody] DiagnozCreateDto dto)
    {
        if (dto == null) return BadRequest();

        using var context = new KlinikaContext();
        var diagnoz = new Diagnoz
        {
            Name = dto.Name,
            Stadia = dto.Stadia,
            VrachId = dto.VrachId,
            UslugaId = dto.UslugaId,
            PetId = dto.PetId,
        };
        context.Diagnozs.Add(diagnoz);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById),
            new { id = diagnoz.Id },
            new DiagnozDto(diagnoz));
    }

    [HttpPut("{id}")]
    public ActionResult<DiagnozDto> Update(int id, [FromBody] DiagnozCreateDto dto)
    {
        if (dto == null) return BadRequest();

        using var context = new KlinikaContext();
        var diagnoz = context.Diagnozs.FirstOrDefault(d => d.Id == id);
        if (diagnoz == null) return NotFound();

        diagnoz.Name = dto.Name;
        diagnoz.Stadia = dto.Stadia;
        diagnoz.VrachId = dto.VrachId;
        diagnoz.UslugaId = dto.UslugaId;
        diagnoz.PetId = dto.PetId;
        context.SaveChanges();

        return Ok(new DiagnozDto(diagnoz));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var context = new KlinikaContext();
        var diagnoz = context.Diagnozs.FirstOrDefault(d => d.Id == id);
        if (diagnoz == null) return NotFound();

        context.Diagnozs.Remove(diagnoz);
        context.SaveChanges();
        return NoContent();
    }
}