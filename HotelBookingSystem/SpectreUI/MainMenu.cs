using HotelBookingSystem.Services;
using HotelBookingSystem.SpectreUI.AdminPanel;
using HotelBookingSystem.SpectreUI.CustomerPanel;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI;

public class MainMenu
{
    private CustomerService customerService;
    private ApartmentService apartmentService;
    private AdminService adminService;

    private AdminLogin adminLogin;

    private CustomerLogin customerLogin;
    private CustomerRegister customerRegister;

    public MainMenu()
    {
        apartmentService = new ApartmentService();
        customerService = new CustomerService(apartmentService);
        adminService = new AdminService(apartmentService, customerService);

        adminLogin = new AdminLogin(adminService);
        customerLogin = new CustomerLogin(customerService);
        customerRegister = new CustomerRegister(customerService);
    }
    #region Run
    public async Task Run()
    {
        while (true)
        {
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Dream[green]House[/]")
                    .PageSize(4)
                    .AddChoices(new[] {
                        "As Customer",
                        "As Administrator\n",
                        "[red]Exit[/]"}));

            switch (choise)
            {
                case "As Customer":
                    AnsiConsole.Clear();
                    await CustomerAsk();
                    break;
                case "As Administrator\n":
                    AnsiConsole.Clear();
                    await AdminAsk();
                    break;
                case "[red]Exit[/]":
                    return;
            }
        }
    }
    #endregion

    #region Customer
    public async Task CustomerAsk()
    {
        while (true)
        {
            var c = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("As Customer")
                .PageSize(4)
                .AddChoices(new[] {
                            "Login",
                            "Register\n",
                            "[red]Go Back[/]"}));
            switch (c)
            {
                case "Login":
                    await customerLogin.Login();
                    break;
                case "Register\n":
                    await customerRegister.Register();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }
    #endregion

    #region Admin
    public async Task AdminAsk()
    {
        while (true)
        {
            var c = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("As Administrator")
                .PageSize(4)
                .AddChoices(new[] {
                            "Login\n",
                            "[red]Go Back[/]"}));
            switch (c)
            {
                case "Login\n":
                    await adminLogin.Login();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }
    #endregion
}
