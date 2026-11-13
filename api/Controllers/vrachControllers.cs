using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.dto;
using api.models;
namespace api.Controllers;
[Route("api/vrachs")]
public class vrachController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<VrachDto> loginVrach(string login)
    {
        var context = new KlinikaContext();
        var vraches = context.Vraches.ToList();
        var vrach = vraches.FirstOrDefault(w => w.Login == login);
        if (vrach == null)
        {
            return BadRequest();
        }
        VrachDto vrachDto = new VrachDto(vrach);
        return Ok(vrachDto);
    }
    
}