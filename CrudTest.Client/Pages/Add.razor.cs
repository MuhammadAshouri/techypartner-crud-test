using CrudTest.Client.Services.Interfaces;
using CrudTest.Domain.Dtos;
using Microsoft.AspNetCore.Components;

namespace CrudTest.Client.Pages;

public partial class Add
{
    private string Messages = "";
    private bool ShowMessage, IsError, ShowButtons = true;
    private readonly UserDto User = new();
    [Inject] public IUserService? UserService { get; set; }

    private async Task SubmitUser()
    {
        if (UserService is not null)
        {
            ShowButtons = false;
            var result = await UserService.AddUser(User);

            ShowButtons = true;
            ShowMessage = true;
            IsError = !result.Item1;
            Messages = result.Item2;
        }
    }
}
