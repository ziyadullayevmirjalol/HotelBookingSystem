using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.AdminPanel.Actions;

public class AdminActions
{
    public AdminActions(Admin admin, AdminService adminService, ApartmentService apartmentService)
    {
        this.admin = admin;
        this.adminService = adminService;
        this.apartmentService = apartmentService;

    }

    private Admin admin;
    private AdminService adminService;
    private ApartmentService apartmentService;

    #region Add new Apartment to the Hotel
    public async Task AddNewApartmentAsync()
    {
        ApartmentCreateModel model = new ApartmentCreateModel();
        var price = AnsiConsole.Ask<double>("Enter [green]Price[/]:");
        var countOfRooms = AnsiConsole.Ask<int>("Enter [green]Count of rooms[/]:");
        AnsiConsole.Markup("Apartment Type \n" +
            "---------------\n" +
            "1. Econo\n" +
            "2. Normal\n" +
            "3. Premium\n" +
            "---------------\n");
    again:
        var type = AnsiConsole.Ask<int>("Choose [green]Apartment Type[/]:");
        switch (type)
        {
            case 1:
                model.ApartmentType = Enums.ApartmentType.Econo;
                break;
            case 2:
                model.ApartmentType = Enums.ApartmentType.Normal;
                break;
            case 3:
                model.ApartmentType = Enums.ApartmentType.Premium;
                break;
            default:
                AnsiConsole.MarkupLine("[red]Invalid choose[/] press any key to try again...");
                Console.ReadLine();
                goto again;
        }

        model.CountOfRooms = countOfRooms;
        model.Price = price;
        try
        {
            Apartment created = await apartmentService.CreateAsync(model);

            var table = new Table();

            table.AddColumn("[yellow]Created Apartment[/]");

            table.AddRow($"[green]Apartment ID[/]: {created.Id}");
            table.AddRow($"[green]Type[/]: {created.ApartmentType}");
            table.AddRow($"[green]Price[/]: {created.Price}");
            table.AddRow($"[green]Count of rooms[/]: {created.CountOfRooms}");

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Press any key to exit...");
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
    }
    #endregion

    #region Delete Apartment from the hotel
    public async Task DeleteApartmentAsync()
    {
        var id = AnsiConsole.Ask<int>("Enter [green]ApartmentID[/]:");
        try
        {
            AnsiConsole.Clear();
            await apartmentService.DeleteAsync(id);
            AnsiConsole.MarkupLine("[green]Apartment Deleted[/] Press enter to exit...");
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
    }
    #endregion

    #region Get Apartment by ID
    public async Task GetApartmentByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Enter [green]ApartmentID[/]:");
        try
        {
            AnsiConsole.Clear();
            var apartment = await adminService.GetApartmentByIdAsync(id);
            var table = new Table();

            table.AddColumn("[yellow]Apartment[/]");

            table.AddRow($"[green]Apartment ID[/]: {apartment.Id}");
            table.AddRow($"[green]Type[/]: {apartment.ApartmentType}");
            table.AddRow($"[green]Price[/]: {apartment.Price}");
            table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("Press enter to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            AnsiConsole.WriteLine("Press any enter to exit...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }
    }
    #endregion

    #region Get All Aparments
    public async Task GetAllApartmentsAsync()
    {
        var apartments = await adminService.GetAllApartmentsAsync();
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
                table.AddRow($"[green]Ordered CustomerID[/]: {apartment.OrderedCustomerId}");
                table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                AnsiConsole.Write(table);
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Get All Customers
    public async Task GetAllCustomersAsync()
    {
        var customers = await adminService.GetAllCustomersAsync();
        if (customers.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The hotel does not yet have customers.[/]\nPress any key to exit...");
            Console.ReadLine();
        }
        else
        {
            foreach (var customer in customers)
            {
                var table = new Table();
                table.AddColumn("[yellow]Your Profile[/]");

                table.AddRow($"[green]Cusomer ID[/]: {customer.Id}");
                table.AddRow($"[green]Username[/]: {customer.Username}");
                table.AddRow($"[green]Email[/]: {customer.Email}");
                table.AddRow($"[green]Firstname[/]: {customer.Firstname}");
                table.AddRow($"[green]Lastname[/]: {customer.Lastname}");
                table.AddRow($"[green]Booked Apartment ID[/]: {customer.ApartmentId}");

                AnsiConsole.Write(table);
                AnsiConsole.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Booked Apartments
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
                table.AddRow($"[green]Ordered CustomerID[/]: {apartment.OrderedCustomerId}");
                table.AddRow($"[green]Count of rooms[/]: {apartment.CountOfRooms}");

                AnsiConsole.Write(table);
            }
            AnsiConsole.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
    #endregion

    #region Not Booked Apartments
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

    #region Hotel's Summary Balance
    public async Task HotelBalanceAsync()
    {
        AnsiConsole.Clear();
        var balance = await adminService.HotelBalanceInfoAsync();
        var table = new Table();

        table.AddColumn("[yellow]Hotel[/]");

        table.AddRow($"[green]Summary Balance ($)[/]: {balance}");

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("Press enter to exit...");
        Console.ReadLine();
        AnsiConsole.Clear();
        return;
    }
    #endregion

    #region Update Admin Password
    public async Task UpdateAdminPasswordAsync()
    {
        var adminmodel = new Admin();
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
        try
        {
            adminmodel.Password = password;
            admin = await adminService.UpdatePasswordAsync(adminmodel);
            AnsiConsole.MarkupLine("[green]Password Changed[/] Press enter to exit...");
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

    }
    #endregion
}
