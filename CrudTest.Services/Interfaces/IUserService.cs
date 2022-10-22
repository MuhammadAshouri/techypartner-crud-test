using CrudTest.Domain.Dtos;

namespace CrudTest.Services.Interfaces;

public interface IUserService
{
    int GetCount(string query);
    UserDto? GetUser(int id);
    IEnumerable<UserDto> GetUsers(int skip, int size, string query);
    Task<(bool success, string data)> AddUser(UserDto model, int? creator = null);
    Task<(bool success, string data)> AddUserRange(IEnumerable<UserDto> models, int? creator = null);
    Task<(bool success, string data)> UpdateUser(UserDto model, int updater);
    Task<(bool success, string data)> UpdateUserStatus(int id, bool status, int updater);
    Task<(bool success, string data)> DeleteUser(int id);
}
