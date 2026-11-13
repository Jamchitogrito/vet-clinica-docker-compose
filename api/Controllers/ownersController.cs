using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.dto;
using api.models;

namespace api.Controllers;

[Route("api/owners")]
public class ownerController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<OwnerDto>> GetAll()
    {
        using var context = new KlinikaContext();
        var entities = context.Owners
            .Include(o => o.Pets)
            .ToList();

        return Ok(entities.Select(o => new OwnerDto(o)).ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<OwnerDto> GetById(int id)
    {
        using var context = new KlinikaContext();
        var owner = context.Owners
            .Include(o => o.Pets)
            .FirstOrDefault(o => o.Id == id);

        if (owner == null) return NotFound();
        return Ok(new OwnerDto(owner));
    }

    [HttpPost]
    public ActionResult<OwnerDto> Create([FromBody] OwnerCreateDto dto)
    {
        if (dto == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(dto.Fio)) return BadRequest("фио обязателен");
        if (string.IsNullOrWhiteSpace(dto.Telefon)) return BadRequest("телефон обязателен");

        using var context = new KlinikaContext();
        var owner = new Owner
        {
            Fio     = dto.Fio,
            Telefon = dto.Telefon,
            Adres   = dto.Adres,
        };
        context.Owners.Add(owner);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById),
            new { id = owner.Id },
            new OwnerDto(owner));
    }

    [HttpPut("{id}")]
    public ActionResult<OwnerDto> Update(int id, [FromBody] OwnerCreateDto dto)
    {
        if (dto == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(dto.Fio)) return BadRequest("фио обязателен");
        if (string.IsNullOrWhiteSpace(dto.Telefon)) return BadRequest("телейфон обязателен");

        using var context = new KlinikaContext();
        var owner = context.Owners.FirstOrDefault(o => o.Id == id);
        if (owner == null) return NotFound();

        owner.Fio     = dto.Fio;
        owner.Telefon = dto.Telefon;
        owner.Adres   = dto.Adres;
        context.SaveChanges();

        return Ok(new OwnerDto(owner));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        using var context = new KlinikaContext();
        var owner = context.Owners
            .Include(o => o.Pets)
            .FirstOrDefault(o => o.Id == id);
        if (owner == null) return NotFound();

        if (owner.Pets.Count > 0)
            return Conflict("У владельца есть питомцы");

        context.Owners.Remove(owner);
        context.SaveChanges();
        return NoContent();
    }
}