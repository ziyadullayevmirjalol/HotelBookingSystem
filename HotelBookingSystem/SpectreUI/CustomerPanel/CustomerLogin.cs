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

                customerMenu = new CustomerMenu(getCustomer);
                await customerMenu.Menu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
