using CrudTest.Client.Models;
using CrudTest.Client.Services.Interfaces;
using CrudTest.Domain.Dtos;
using Microsoft.AspNetCore.Components;

namespace CrudTest.Client.Pages;

public partial class Index
{
    private IEnumerable<UserDto>? Users;
    private int Page = 1, TotalCount;
    private const int Size = 12;
    private readonly SearchModel SearchModel = new();
    private string LastSearch = "";

    [Inject] public IUserService? UserService { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        TotalCount = await UserService!.GetUsersCount(SearchModel.Text);
        Users = await UserService!.GetUsers(Page, Size, SearchModel.Text);
        Page = 1;
    }

    private async Task FetchNewData(int page)
    {
        Page = page;
        if (LastSearch != SearchModel.Text.Trim())
        {
            LastSearch = SearchModel.Text;
            TotalCount = await UserService!.GetUsersCount(SearchModel.Text);
        }
        Users = await UserService!.GetUsers(Page, Size, SearchModel.Text);
        StateHasChanged();
    }
}
