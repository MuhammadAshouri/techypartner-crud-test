using CrudTest.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CrudTest.Client.Pages;

public partial class AddJson
{
    private string Messages = "", FileData = "";
    private bool ShowMessage, IsError, ShowButtons = true;
    [Inject] public IUserService? UserService { get; set; }

    private async Task SubmitUsers()
    {
        if (UserService is not null)
        {
            if (FileData != "")
            {
                ShowButtons = false;
                var result = await UserService.AddUserJson(FileData);

                ShowButtons = true;
                ShowMessage = true;
                IsError = !result.Item1;
                Messages = result.Item2;
            }
            else
            {
                ShowMessage = true;
                IsError = true;
                Messages = "File is empty";
            }
        }
    }

    private async Task FileInputChanged(InputFileChangeEventArgs obj)
    {
        var stream = obj.File.OpenReadStream();
        var tmp = new StreamReader(stream);
        FileData = await tmp.ReadToEndAsync();
        FileData = FileData.Trim();
    }
}
