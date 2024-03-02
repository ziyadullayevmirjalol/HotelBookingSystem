using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HotelBookingSystem.SpectreUI.CustomerPanel;

public class CustomerRegister
{
    private CustomerService customerService;
    private CustomerMenu customerMenu;
    public CustomerRegister(CustomerService customerService)
    {
        this.customerService = customerService;
    }
    public async Task Register()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var customerCreateModel = new CustomerCreateModel();

            var username = AnsiConsole.Ask<string>("Enter your [green]username[/]:");
        reenterpassword:
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your [green]password[/]:")
                    .PromptStyle("yellow")
            .Secret());
            while (password.Length < 8)
            {
                AnsiConsole.WriteLine("Password's length must be at least 8 characters");
                goto reenterpassword;
            }
            string email = string.Empty;
            while (String.IsNullOrWhiteSpace(email = AnsiConsole.Ask<string>("Enter your [green]email[/]")) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Console.WriteLine("Invalid email address!");
            }
            string firstname = AnsiConsole.Ask<string>("Enter your [green]Firstname[/]");
            string lastname = AnsiConsole.Ask<string>("Enter your [green]Lastname[/]");

            customerCreateModel.Username = username;
            customerCreateModel.Password = password;
            customerCreateModel.Email = email;
            customerCreateModel.Firstname = firstname;
            customerCreateModel.Lastname = lastname;

            try
            {
                var createdCustomer = await customerService.Create(customerCreateModel);
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
