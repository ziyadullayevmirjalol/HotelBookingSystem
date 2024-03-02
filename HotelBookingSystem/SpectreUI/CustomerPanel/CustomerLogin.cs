using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.CustomerPanel;

public class CustomerLogin
{
    private CustomerService customerService;
    private CustomerMenu customerMenu;

    public CustomerLogin(CustomerService customerService)
    {
        this.customerService = customerService;

    }
    public async Task Login()
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
                var getCustomer = await customerService.GetToLogin(username, password);

                customerMenu = new CustomerMenu(getCustomer, customerService);
                await customerMenu.Menu();
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
}
