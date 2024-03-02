using HotelBookingSystem.Extensions;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HotelBookingSystem.SpectreUI.CustomerPanel.Actions;

public class CustomerActions
{
    private Customer Customer;
    private CustomerService customerService;
    public CustomerActions(Customer customer, CustomerService customerService)
    {
        this.Customer = customer;
        this.customerService = customerService;
    }
    #region Deposit
    public async Task Deposit()
    {
        var amount = AnsiConsole.Ask<double>("Enter [green]amount[/]: ");
        await AnsiConsole.Status()
     .Start("Process...", async ctx =>
     {
         AnsiConsole.MarkupLine("loading services...");
         try
         {
             Customer = await customerService.Deposit(Customer.Id, amount);
         }
         catch (Exception ex)
         {
             AnsiConsole.WriteLine(ex.Message);
         }
         Thread.Sleep(1000);
         AnsiConsole.Clear();
     });

        AnsiConsole.MarkupLine("[green]Done[/] Press any key to continue...");
        Console.ReadLine();
    }
    #endregion

    #region Book New Apartment
    public async Task BookNewApartment()
    {
        var apartmentId = AnsiConsole.Ask<int>("Enter [green]apartment ID[/]: ");
        await AnsiConsole.Status()
     .Start("Process...", async ctx =>
     {
         AnsiConsole.MarkupLine("loading services...");
         try
         {
             Customer = await customerService.BookApartment(apartmentId, Customer.Id);
             AnsiConsole.MarkupLine("[green]You Booked a new apartment[/] Press any key to continue...");
             Console.ReadLine();
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
         Thread.Sleep(2000);
         AnsiConsole.Clear();
     });
    }
    #endregion

    #region Remove Booked Apartment
    public async Task RemoveBookedApartment()
    {
    reenter:
        var mapped = Customer.MapTo<CustomerViewModel>();
        var table = new Table();

        table.AddColumn("[yellow]Your Profile[/]");

        table.AddRow($"[green]Cusomer ID[/]: {mapped.Id}");
        table.AddRow($"[green]Username[/]: {mapped.Username}");
        table.AddRow($"[green]Email[/]: {mapped.Email}");
        table.AddRow($"[green]Balance ($)[/]: {mapped.Balance}");
        table.AddRow($"[green]Firstname[/]: {mapped.Firstname}");
        table.AddRow($"[green]Lastname[/]: {mapped.Lastname}");
        table.AddRow($"[green]Booked Apartment ID[/]: {mapped.ApartmentId}");

        AnsiConsole.Write(table);

        if (mapped.ApartmentId != 0)
        {
            AnsiConsole.WriteLine($"Are you sure you want to remove Booked Apartment with ID: {mapped.ApartmentId}?...");
            AnsiConsole.Write("Press (yes) to confirm, (no) to cancel:");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "yes":
                    try
                    {
                        Customer = await customerService.DeleteBookingApartment(mapped.ApartmentId, mapped.Id);
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
                    break;
                case "no":
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[green]Canceled[/]");
                    Thread.Sleep(1000);
                    return;
                default:
                    Console.WriteLine("invalid input");
                    await Console.Out.WriteLineAsync("Press any key to reenter...");
                    Console.ReadLine();
                    goto reenter;
            }
        }
        else
        {
            AnsiConsole.WriteLine($"You are not booked any apartment yet.");
            AnsiConsole.WriteLine("Press any key.");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region View Profile
    public async Task ViewProfile()
    {
        var mapped = Customer.MapTo<CustomerViewModel>();
        var table = new Table();

        table.AddColumn("[yellow]Your Profile[/]");

        table.AddRow($"[green]Cusomer ID[/]: {mapped.Id}");
        table.AddRow($"[green]Username[/]: {mapped.Username}");
        table.AddRow($"[green]Email[/]: {mapped.Email}");
        table.AddRow($"[green]Balance ($)[/]: {mapped.Balance}");
        table.AddRow($"[green]Firstname[/]: {mapped.Firstname}");
        table.AddRow($"[green]Lastname[/]: {mapped.Lastname}");
        table.AddRow($"[green]Booked Apartment ID[/]: {mapped.ApartmentId}");

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
    #endregion

    #region Update Customer Details
    public async Task UpdateCustomerDetails()
    {
        CustomerUpdateModel customerUpdate = new CustomerUpdateModel();

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

        customerUpdate.Username = username;
        customerUpdate.Password = password;
        customerUpdate.Email = email;
        customerUpdate.Firstname = firstname;
        customerUpdate.Lastname = lastname;

        try
        {
            await customerService.Update(Customer.Id, customerUpdate);
            Customer = await customerService.GetCustomer(Customer.Id);
            AnsiConsole.MarkupLine("[green]Success[/] Press any key to continue...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region Delete Account
    public async Task DeleteAccount()
    {
    reenter:
        AnsiConsole.WriteLine($"Are you sure you want to delete your account with username: {Customer.Username}?...");
        AnsiConsole.Write("Press (yes) to confirm, (no) to cancel:");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "yes":
                try
                {
                    AnsiConsole.Clear();
                    await customerService.Delete(Customer.Id);
                    AnsiConsole.MarkupLine("[green]Success[/]Press any key to exit...");
                    Console.ReadLine();
                    AnsiConsole.Clear();
                    return;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    AnsiConsole.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                    AnsiConsole.Clear();
                    return;
                }
            case "no":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Canceled[/]");
                Thread.Sleep(1000);
                return;
            default:
                Console.WriteLine("invalid input");
                await Console.Out.WriteLineAsync("Press any key to reenter...");
                Console.ReadLine();
                goto reenter;
        }
    }
    #endregion
}
