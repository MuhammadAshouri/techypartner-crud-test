using CrudTest.Domain.Dtos;

namespace CrudTest.Client.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsers(int page, int size, string query);
    Task<int> GetUsersCount(string query);
    Task<UserDto?> GetUser(int id);
    Task<(bool, string)> AddUser(UserDto model);
    Task<(bool, string)> AddUserJson(string data);
    Task<(bool, string)> UpdateUser(int id, UserDto model);
    Task<(bool, string)> UpdateUserStatus(int id, bool status);
    Task<(bool, string)> RemoveUser(int id);
}
