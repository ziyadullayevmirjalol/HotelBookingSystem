using HotelBookingSystem.Helpers;
using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.CustomerPanel;

public class CustomerLogin
{

    private CustomerService customerService;
    private ApartmentService apartmentService;
    private CustomerMenu customerMenu;

    public CustomerLogin(CustomerService customerService, ApartmentService apartmentService)
    {
        this.customerService = customerService;
        this.apartmentService = apartmentService;
    }

    #region Login
    public async Task LoginAync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var username = AnsiConsole.Ask<string>("Enter your [green]username[/]:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your [green]password[/]:")
                    .PromptStyle("yellow")
                    .Secret());

            try
            {
                var getCustomer = await customerService.GetToLoginAsync(username, password);

                customerMenu = new CustomerMenu(getCustomer, customerService, apartmentService);
                await customerMenu.MenuAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                AnsiConsole.WriteLine("Press any key to exit and try again.");
                Console.ReadLine();
                AnsiConsole.Clear();
                return;
            }
        }
    }
    #endregion
}
