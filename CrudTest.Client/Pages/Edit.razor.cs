using CrudTest.Client.Services.Interfaces;
using CrudTest.Domain.Dtos;
using Microsoft.AspNetCore.Components;

namespace CrudTest.Client.Pages;

public partial class Edit
{
    private string Messages = "";
    private bool ShowMessage, IsError, ShowButtons = true;
    private UserDto? User;
    [Parameter] public int Id { get; set; }
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

    private async Task SubmitUser()
    {
        if (UserService is not null)
        {
            if (User is not null)
            {
                ShowButtons = false;
                var result = await UserService.UpdateUser(Id, User);

                ShowButtons = true;
                ShowMessage = true;
                IsError = !result.Item1;
                Messages = result.Item2;
            }
            else NavManager?.NavigateTo("/");
        }
    }

    private async Task SubmitStatus(ChangeEventArgs args)
    {
        if (UserService is not null)
        {
            ShowButtons = false;
            var result = await UserService.UpdateUserStatus(Id, (bool?)args.Value ?? false);

            ShowButtons = true;
            ShowMessage = !result.Item1;
            IsError = !result.Item1;
            Messages = result.Item2;
        }
    }
}
