using HotelBookingSystem.Presentation.SpectreUI.AdminPanel;
using HotelBookingSystem.Presentation.SpectreUI.CustomerPanel;
using HotelBookingSystem.Services.Services;
using Spectre.Console;

namespace HotelBookingSystem.Presentation.SpectreUI;

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

        adminLogin = new AdminLogin(adminService, apartmentService);
        customerLogin = new CustomerLogin(customerService, apartmentService);
        customerRegister = new CustomerRegister(customerService, apartmentService);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Dream[green]House[/]")
                    .PageSize(4)
                    .AddChoices(new[] {
                    "As Customer",
                    "As Administrator\n",
                    "[red]Exit[/]"}));

            switch (choice)
            {
                case "As Customer":
                    AnsiConsole.Clear();
                    await CustomerMenuAsync();
                    break;
                case "As Administrator\n":
                    AnsiConsole.Clear();
                    await AdminMenuAsync();
                    break;
                case "[red]Exit[/]":
                    return;
            }
        }
    }

    public async Task CustomerMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("As Customer")
                    .PageSize(4)
                    .AddChoices(new[] {
                    "Login",
                    "Register\n",
                    "[red]Go Back[/]"}));

            switch (choice)
            {
                case "Login":
                    await customerLogin.LoginAsync();
                    break;
                case "Register\n":
                    await customerRegister.RegisterAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }

    public async Task AdminMenuAsync()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("As Administrator")
                    .PageSize(4)
                    .AddChoices(new[] {
                    "Login\n",
                    "[red]Go Back[/]"}));

            switch (choice)
            {
                case "Login\n":
                    await adminLogin.LoginAsync();
                    break;
                case "[red]Go Back[/]":
                    return;
            }
        }
    }


    //#region Run
    //public async Task RunAsync()
    //{
    //    while (true)
    //    {
    //        var choise = AnsiConsole.Prompt(
    //            new SelectionPrompt<string>()
    //                .Title("Dream[green]House[/]")
    //                .PageSize(4)
    //                .AddChoices(new[] {
    //                    "As Customer",
    //                    "As Administrator\n",
    //                    "[red]Exit[/]"}));

    //        switch (choise)
    //        {
    //            case "As Customer":
    //                AnsiConsole.Clear();
    //                await CustomerAskAsync();
    //                break;
    //            case "As Administrator\n":
    //                AnsiConsole.Clear();
    //                await AdminAskAsync();
    //                break;
    //            case "[red]Exit[/]":
    //                return;
    //        }
    //    }
    //}
    //#endregion

    //#region Customer
    //public async Task CustomerAskAsync()
    //{
    //    while (true)
    //    {
    //        var c = AnsiConsole.Prompt(
    //        new SelectionPrompt<string>()
    //            .Title("As Customer")
    //            .PageSize(4)
    //            .AddChoices(new[] {
    //                        "Login",
    //                        "Register\n",
    //                        "[red]Go Back[/]"}));
    //        switch (c)
    //        {
    //            case "Login":
    //                await customerLogin.LoginAync();
    //                break;
    //            case "Register\n":
    //                await customerRegister.RegisterAsync();
    //                break;
    //            case "[red]Go Back[/]":
    //                return;
    //        }
    //    }
    //}
    //#endregion

    //#region Admin
    //public async Task AdminAskAsync()
    //{
    //    while (true)
    //    {
    //        var c = AnsiConsole.Prompt(
    //        new SelectionPrompt<string>()
    //            .Title("As Administrator")
    //            .PageSize(4)
    //            .AddChoices(new[] {
    //                        "Login\n",
    //                        "[red]Go Back[/]"}));
    //        switch (c)
    //        {
    //            case "Login\n":
    //                await adminLogin.LoginAsync();
    //                break;
    //            case "[red]Go Back[/]":
    //                return;
    //        }
    //    }
    //}
    //#endregion
}
