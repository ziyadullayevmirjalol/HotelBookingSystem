using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.AdminPanel;

public class AdminLogin
{
    private AdminService adminService;
    private AdminMenu adminMenu;

    public AdminLogin(AdminService adminService)
    {
        this.adminService = adminService;
    }
    public async Task Login()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]:")
            .PromptStyle("yellow")
            .Secret());

            try
            {
                var getAdmin = await adminService.LoginAsAdmin(password);

                adminMenu = new AdminMenu(getAdmin);
                await adminMenu.Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
