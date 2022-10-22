using System.Collections.ObjectModel;
using AutoMapper;
using CrudTest.Data.Interfaces;
using CrudTest.Domain.Dtos;
using CrudTest.Domain.Models;
using CrudTest.Services.Interfaces;
using CrudTest.Services.Validations;

namespace CrudTest.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository Repo;
    private readonly IMapper Mapper;
    private readonly IUnitOfWork UoW;

    public UserService(IUserRepository repo, IMapper mapper, IUnitOfWork uow)
    {
        Repo = repo;
        Mapper = mapper;
        UoW = uow;
    }

    public int GetCount(string query) => Repo.Count(c => c.FirstName.Contains(query) || c.LastName.Contains(query) || c.Email.Contains(query));

    public UserDto? GetUser(int id) => Mapper.Map<UserDto>(Repo.GetById(id));

    public IEnumerable<UserDto> GetUsers(int skip, int size, string query) =>
        Mapper.Map<IEnumerable<UserDto>>(Repo.Find(c =>
            c.FirstName.Contains(query) || c.LastName.Contains(query) || c.Email.Contains(query)).Skip(skip).Take(size));

    public async Task<(bool success, string data)> AddUser(UserDto model, int? creator = null)
    {
        var validator = new UserValidator();
        var validateResult = await validator.ValidateAsync(model);
        if (!validateResult.IsValid) return (false, string.Join(", ", validateResult.Errors));
        
        User user = new()
        {
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Email = model.Email.Trim(),
            Gender = model.Gender.Trim(),
            Status = model.Status,
            CreatedDate = DateTime.Now
        };
        if (creator is not null) user.CreatedBy = creator;
        Repo.Add(user);
        var result = await UoW.Complete();
        return (result, result ? user.Id.ToString() : "Error saving user...");
    }

    public async Task<(bool success, string data)> AddUserRange(IEnumerable<UserDto> models, int? creator = null)
    {
        ICollection<User> users = new Collection<User>();
        
        foreach (var item in models)
        {
            var validator = new UserValidator();
            var validateResult = await validator.ValidateAsync(item);
            if (!validateResult.IsValid) return (false, string.Join(", ", validateResult.Errors) + "|" + item.Id);
            
            User tmp = new()
            {
                FirstName = item.FirstName.Trim(),
                LastName = item.LastName.Trim(),
                Email = item.Email.Trim(),
                Gender = item.Gender.Trim(),
                Status = item.Status,
                CreatedDate = DateTime.Now
            };
            
            if (creator is not null) tmp.CreatedBy = creator;
            users.Add(tmp);
        }
        
        Repo.AddRange(users);
        var result = await UoW.Complete();
        return (result, result ? "" : "Error saving users...");
    }

    public async Task<(bool success, string data)> UpdateUser(UserDto model, int updater)
    {
        var user = Repo.GetById(model.Id);
        if (user is null) return (false, "User not found");

        var validator = new UserValidator();
        var validateResult = await validator.ValidateAsync(model);
        if (!validateResult.IsValid) return (false, string.Join(", ", validateResult.Errors));

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        user.Email = model.Email.Trim();
        user.Gender = model.Gender.Trim();
        user.UpdatedDate = DateTime.Now;
        user.UpdatedBy = updater;
        
        Repo.Update(user);
        var result = await UoW.Complete();
        return (result, result ? user.Id.ToString() : "Error updating user...");
    }

    public async Task<(bool success, string data)> UpdateUserStatus(int id, bool status, int updater)
    {
        var user = Repo.GetById(id);
        if (user is null) return (false, "User not found");

        user.Status = status;
        user.UpdatedDate = DateTime.Now;
        user.UpdatedBy = updater;
        
        Repo.Update(user);
        var result = await UoW.Complete();
        return (result, result ? user.Id.ToString() : "Error updating user...");
    }

    public async Task<(bool success, string data)> DeleteUser(int id)
    {
        var user = Repo.GetById(id);
        if (user is null) return (false, "User not found");
        Repo.Remove(user);
        var result = await UoW.Complete();
        return (result, result ? id.ToString() : "Error deleting user...");
    }
}
