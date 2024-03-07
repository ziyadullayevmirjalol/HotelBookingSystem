using HotelBookingSystem.DTOs.CustomerModels;
using HotelBookingSystem.Services.Helpers;
using HotelBookingSystem.Services.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HotelBookingSystem.Presentation.SpectreUI.CustomerPanel;

public class CustomerRegister
{
    private CustomerService customerService;
    private ApartmentService apartmentService;
    private CustomerMenu customerMenu;
    public CustomerRegister(CustomerService customerService, ApartmentService apartmentService)
    {
        this.customerService = customerService;
        this.apartmentService = apartmentService;
    }

    #region Registration
    public async Task RegisterAsync()
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
            while (string.IsNullOrWhiteSpace(email = AnsiConsole.Ask<string>("Enter your [green]email[/]")) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Console.WriteLine("Invalid email address!");
            }
            string firstname = AnsiConsole.Ask<string>("Enter your [green]Firstname[/]");
            string lastname = AnsiConsole.Ask<string>("Enter your [green]Lastname[/]");

            var HashedPassword = PasswordHashing.Hashing(password);

            customerCreateModel.Username = username;
            customerCreateModel.Password = HashedPassword;
            customerCreateModel.Email = email;
            customerCreateModel.Firstname = firstname;
            customerCreateModel.Lastname = lastname;

            try
            {
                var createdCustomer = await customerService.CreateAsync(customerCreateModel);
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
