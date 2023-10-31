using Microsoft.AspNetCore.Mvc;
using RebarAPI.Data;
using RebarAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly MongoService _mongoService;

    public MenuController(MongoService mongoService)
    {
        _mongoService = mongoService;
    }

    // GET: api/Menu
    [HttpGet]
    public ActionResult<List<Shake>> Get()
    {
        return _mongoService.GetMenu();
    }

    // GET: api/Menu/{id}
    [HttpGet("{id:length(24)}", Name = "GetShake")]
    public ActionResult<Shake> Get(string id)
    {
        var shake = _mongoService.GetShakeById(new Guid(id));

        if (shake == null)
        {
            return NotFound();
        }

        return shake;
    }

    // POST: api/Menu
    [HttpPost]
    public ActionResult<Shake> Create(Shake shake)
    {
        _mongoService.CreateShake(shake);

        return CreatedAtRoute("GetShake", new { id = shake.Id.ToString() }, shake);
    }

    // PUT: api/Menu/{id}
    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Shake shakeIn)
    {
        var shake = _mongoService.GetShakeById(new Guid(id));

        if (shake == null)
        {
            return NotFound();
        }

        _mongoService.UpdateShake(new Guid(id), shakeIn);

        return NoContent();
    }

    // DELETE: api/Menu/{id}
    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var shake = _mongoService.GetShakeById(new Guid(id));

        if (shake == null)
        {
            return NotFound();
        }

        _mongoService.DeleteShakeById(new Guid(id));

        return NoContent();
    }
}
