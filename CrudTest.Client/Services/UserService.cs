using System.Net.Http.Json;
using CrudTest.Client.Models;
using CrudTest.Client.Services.Interfaces;
using CrudTest.Domain.Dtos;

namespace CrudTest.Client.Services;

public class UserService : IUserService
{
    private readonly HttpClient Client;
    public UserService(HttpClient client) => Client = client;

    public async Task<IEnumerable<UserDto>> GetUsers(int page, int size, string query) =>
        await Client.GetFromJsonAsync<IEnumerable<UserDto>>($"{Consts.ApiBase}Users/GetUsersRange?page={page}&size={size}&query={query}") ??
        Enumerable.Empty<UserDto>();

    public async Task<int> GetUsersCount(string query = "") => int.Parse(await Client.GetStringAsync($"{Consts.ApiBase}Users/GetUsersCount?query={query}"));

    public async Task<UserDto?> GetUser(int id) => await Client.GetFromJsonAsync<UserDto>($"{Consts.ApiBase}Users/GetUser?id={id}");

    public async Task<(bool, string)> AddUser(UserDto model)
    {
        var result = await Client.PostAsJsonAsync($"{Consts.ApiBase}Users/AddUser", model);
        return (result.IsSuccessStatusCode, result.IsSuccessStatusCode ? "" : await result.Content.ReadAsStringAsync());
    }
    
    public async Task<(bool, string)> AddUserJson(string data)
    {
        var result = await Client.PostAsJsonAsync($"{Consts.ApiBase}Users/AddUserFromJson", data);
        return (result.IsSuccessStatusCode, result.IsSuccessStatusCode ? "" : await result.Content.ReadAsStringAsync());
    }

    public async Task<(bool, string)> UpdateUser(int id, UserDto model)
    {
        var result = await Client.PutAsJsonAsync($"{Consts.ApiBase}Users/UpdateUser?id={id}", model);
        return (result.IsSuccessStatusCode, result.IsSuccessStatusCode ? "" : await result.Content.ReadAsStringAsync());
    }

    public async Task<(bool, string)> UpdateUserStatus(int id, bool status)
    {
        var result = await Client.PutAsync($"{Consts.ApiBase}Users/UpdateUserStatus?id={id}&status={status}", null);
        return (result.IsSuccessStatusCode, result.IsSuccessStatusCode ? "" : await result.Content.ReadAsStringAsync());
    }

    public async Task<(bool, string)> RemoveUser(int id)
    {
        var result = await Client.DeleteAsync($"{Consts.ApiBase}Users/DeleteUser?id={id}");
        return (result.IsSuccessStatusCode, result.IsSuccessStatusCode ? "" : await result.Content.ReadAsStringAsync());
    }
}
