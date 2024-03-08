using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.DTOs.CustomerModels;
using HotelBookingSystem.Services.Extensions;
using HotelBookingSystem.Services.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HotelBookingSystem.Presentation.SpectreUI.CustomerPanel.Actions;

public class CustomerActions
{
    private Customer Customer;
    private CustomerService customerService;
    private ApartmentService apartmentService;
    public CustomerActions(Customer customer, CustomerService customerService, ApartmentService apartmentService)
    {
        Customer = customer;
        this.customerService = customerService;
        this.apartmentService = apartmentService;
    }

    #region Deposit
    public async Task DepositAsync()
    {
        var amount = AnsiConsole.Ask<double>("Enter [green]amount[/]: ");
        await AnsiConsole.Status()
     .Start("Process...", async ctx =>
     {
         AnsiConsole.MarkupLine("loading services...");
         try
         {
             Customer = await customerService.DepositAsync(Customer.Id, amount);
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

    #region View Booked Apartments
    public async Task BookedApartmentsAsync()
    {
        var apartments = await apartmentService.BookedApartmentsAsync();
        if (apartments.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have apartments.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var apartment in apartments)
            {
                var table = new Table();

                table.AddColumn("[yellow]Apartment[/]");

                table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                table.AddRow($"[green]Price[/]: {apartment.Price}");
                table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                AnsiConsole.Write(table);
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region View Not Booked Apartments
    public async Task NotBookedApartmentsAsync()
    {
        var apartments = await apartmentService.NotBookedApartmentsAsync();
        if (apartments.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have apartments.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var apartment in apartments)
            {
                var table = new Table();

                table.AddColumn("[yellow]Apartment[/]");

                table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                table.AddRow($"[green]Price[/]: {apartment.Price}");
                table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                AnsiConsole.Write(table);
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Book New Apartment
    public async Task BookNewApartmentAsync()
    {
        var apartmentId = AnsiConsole.Ask<int>("Enter [green]apartment ID[/]: ");
        await AnsiConsole.Status()
     .Start("Process...", async ctx =>
     {
         AnsiConsole.MarkupLine("loading services...");
         try
         {
             AnsiConsole.Clear();
             Customer = await customerService.BookApartmentAsync(apartmentId, Customer.Id);
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
         AnsiConsole.Clear();
     });
    }
    #endregion

    #region Get All Econo Class Apartments
    public async Task GetAllEconoApartmentsAsync()
    {
        var apartments = await apartmentService.GetAllPremiumAsync();
        if (apartments.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have apartments.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var apartment in apartments)
            {
                if (apartment.OrderedCustomerId == 0)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {"Not Booked Yet"}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }
                else
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {apartment.OrderedCustomerId}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }

            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Get All Normal Class Apartment
    public async Task GetAllNormalApartmentsAsync()
    {
        var apartments = await apartmentService.GetAllNormalAsync();
        if (apartments.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have apartments.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var apartment in apartments)
            {

                if (apartment.OrderedCustomerId == 0)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {"Not Booked Yet"}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }
                else
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {apartment.OrderedCustomerId}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Get All Premium Class Apartments
    public async Task GetAllPremiumApartmentsAsync()
    {
        var apartments = await apartmentService.GetAllPremiumAsync();
        if (apartments.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have apartments.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var apartment in apartments)
            {

                if (apartment.OrderedCustomerId == 0)
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {"Not Booked Yet"}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }
                else
                {
                    var table = new Table();

                    table.AddColumn("[yellow]Apartment[/]");

                    table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
                    table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
                    table.AddRow($"[green]Price[/]: {apartment.Price}");
                    table.AddRow($"[green]Ordered CustomerID[/]: {apartment.OrderedCustomerId}");
                    table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                    AnsiConsole.Write(table);
                }
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Remove Booked Apartment
    public async Task RemoveBookedApartmentAsync()
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
                        Customer = await customerService.DeleteBookingApartmentAsync(mapped.ApartmentId, mapped.Id);
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
    public async Task ViewProfileAsync()
    {
        var customerView = await customerService.ViewCustomerAsync(Customer.Id);
        var table = new Table();
        if (customerView.ApartmentId != 0)
        {
            table.AddColumn("[yellow]Your Profile[/]");

            table.AddRow($"[green]Cusomer ID[/]: {customerView.Id}");
            table.AddRow($"[green]Username[/]: {customerView.Username}");
            table.AddRow($"[green]Email[/]: {customerView.Email}");
            table.AddRow($"[green]Balance ($)[/]: {customerView.Balance}");
            table.AddRow($"[green]Firstname[/]: {customerView.Firstname}");
            table.AddRow($"[green]Lastname[/]: {customerView.Lastname}");
            table.AddRow($"[green]Booked Apartment ID[/]: {customerView.ApartmentId}");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        else
        {
            table.AddColumn("[yellow]Your Profile[/]");

            table.AddRow($"[green]Cusomer ID[/]: {customerView.Id}");
            table.AddRow($"[green]Username[/]: {customerView.Username}");
            table.AddRow($"[green]Email[/]: {customerView.Email}");
            table.AddRow($"[green]Balance ($)[/]: {customerView.Balance}");
            table.AddRow($"[green]Firstname[/]: {customerView.Firstname}");
            table.AddRow($"[green]Lastname[/]: {customerView.Lastname}");
            table.AddRow($"[green]Booked Apartment ID[/]: {"Not ordered yet"}");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Update Customer Details
    public async Task UpdateCustomerDetailsAsync()
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
        while (string.IsNullOrWhiteSpace(email = AnsiConsole.Ask<string>("Enter your [green]email[/]")) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
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
            await customerService.UpdateAsync(Customer.Id, customerUpdate);
            Customer = await customerService.GetCustomerAsync(Customer.Id);
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
    public async Task DeleteAccountAsync()
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
                    await customerService.DeleteAsync(Customer.Id);
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
