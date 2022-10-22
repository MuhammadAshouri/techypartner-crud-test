using CrudTest.Client.Services.Interfaces;
using CrudTest.Domain.Dtos;
using Microsoft.AspNetCore.Components;

namespace CrudTest.Client.Pages;

public partial class View
{
    [Parameter] public int Id { get; set; }
    private bool ShowError;
    private string Error = "";
    private UserDto? User;
    [Inject] public IUserService? UserService { get; set; }
    [Inject] public NavigationManager? NavManager { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (UserService is not null)
        {
            User = await UserService.GetUser(Id);
            if (User is null) NavManager?.NavigateTo("/");
        }
    }

    private async Task DeleteItem()
    {
        if (UserService is not null)
        {
            var result = await UserService.RemoveUser(Id);
            if (result.Item1) NavManager?.NavigateTo("/");
            else
            {
                ShowError = true;
                Error = result.Item2;
            }
        }
    }
}
