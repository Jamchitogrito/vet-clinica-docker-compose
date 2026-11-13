using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.dto;
using api.models;

namespace api.Controllers;

[Route("api/pets")]
public class petController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PetDto>> GetAll()
    {
        using var context = new KlinikaContext();
        var entities = context.Pets
            .Include(p => p.Owner)
            .ToList();

        return Ok(entities.Select(p => new PetDto(p)).ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<PetDto> GetById(int id)
    {
        using var context = new KlinikaContext();
        var pet = context.Pets
            .Include(p => p.Owner)
            .FirstOrDefault(p => p.Id == id);

        if (pet == null) return NotFound();
        return Ok(new PetDto(pet));
    }

    [HttpPost]
    public ActionResult<PetDto> Create([FromBody] PetCreateDto dto)
    {
        if (dto == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(dto.Klichka)) return BadRequest("кличка обязательна");
        if (dto.OwnerId <= 0) return BadRequest("владелец обязателен");

        using var context = new KlinikaContext();
        if (!context.Owners.Any(o => o.Id == dto.OwnerId))
            return BadRequest("владелец не найден");

        var pet = new Pet
        {
            Klichka = dto.Klichka,
            Vid     = dto.Vid,
            Poroda  = dto.Poroda,
            Vozrast = dto.Vozrast,
            OwnerId = dto.OwnerId,
        };
        context.Pets.Add(pet);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById),
            new { id = pet.Id },
            new PetDto(pet));
    }

    [HttpPut("{id}")]
    public ActionResult<PetDto> Update(int id, [FromBody] PetCreateDto dto)
    {
        if (dto == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(dto.Klichka)) return BadRequest("кличка обязательна");
        if (dto.OwnerId <= 0) return BadRequest("владелец обязателен");

        using var context = new KlinikaContext();
        var pet = context.Pets.FirstOrDefault(p => p.Id == id);
        if (pet == null) return NotFound();

        if (!context.Owners.Any(o => o.Id == dto.OwnerId))
            return BadRequest("владелец не найден");

        pet.Klichka = dto.Klichka;
        pet.Vid     = dto.Vid;
        pet.Poroda  = dto.Poroda;
        pet.Vozrast = dto.Vozrast;
        pet.OwnerId = dto.OwnerId;
        context.SaveChanges();

        return Ok(new PetDto(pet));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        using var context = new KlinikaContext();
        var pet = context.Pets.FirstOrDefault(p => p.Id == id);
        if (pet == null) return NotFound();
        var hasDiagnoz = context.Diagnozs.Any(d => d.PetId == id);
        if (hasDiagnoz)
            return Conflict("У питомца есть диагнозы");

        context.Pets.Remove(pet);
        context.SaveChanges();
        return NoContent();
    }
}