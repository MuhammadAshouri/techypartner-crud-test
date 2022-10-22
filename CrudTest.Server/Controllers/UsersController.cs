using System.Collections.ObjectModel;
using System.Text.Json;
using CrudTest.Domain.Dtos;
using CrudTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudTest.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService UserService;
    public UsersController(IUserService userService) => UserService = userService;
    
    [HttpGet]
    public IActionResult GetUsersRange(int page, int size, string? query = null) =>
        Ok(UserService.GetUsers(page * size - size, size, query ?? ""));

    [HttpGet]
    public IActionResult GetUsersCount(string? query = null) => Ok(UserService.GetCount(query ?? ""));
    
    [HttpGet]
    public IActionResult GetUser(int id) => Ok(UserService.GetUser(id));

    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto model)
    {
        var result = await UserService.AddUser(model);
        return result.success ? Ok(result.data) : BadRequest(result.data);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserFromJson([FromBody] string json)
    {
        var result = await UserService.AddUserRange(
            JsonSerializer.Deserialize<ICollection<UserDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ??
            new Collection<UserDto>());
        return result.success ? Ok(result.data) : BadRequest(result.data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id, UserDto model)
    {
        model.Id = id;
        var result = await UserService.UpdateUser(model, 0);
        return result.success ? Ok(result.data) : BadRequest(result.data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserStatus(int id, bool status)
    {
        var result = await UserService.UpdateUserStatus(id, status, 0);
        return result.success ? Ok(result.data) : BadRequest(result.data);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await UserService.DeleteUser(id);
        return result.success ? Ok(result.data) : BadRequest(result.data);
    }
}
