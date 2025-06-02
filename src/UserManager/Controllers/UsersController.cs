using Microsoft.AspNetCore.Mvc;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _service.GetById(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public IActionResult Add(User user)
    {
        _service.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        return _service.Update(id, user) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}
