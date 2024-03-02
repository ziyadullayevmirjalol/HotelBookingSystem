using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.AdminPanel.Actions;

public class AdminActions
{
    private Admin admin;
    private AdminService adminService;
    private ApartmentService apartmentService;
    public AdminActions(Admin admin, AdminService adminService, ApartmentService apartmentService)
    {
        this.admin = admin;
        this.adminService = adminService;
        this.apartmentService = apartmentService;

    }
    public async Task AddNewApartment()
    {
        ApartmentCreateModel model = new ApartmentCreateModel();
        var price = AnsiConsole.Ask<double>("Enter [green]Price[/]:");
        var countOfRooms = AnsiConsole.Ask<int>("Enter [green]Count of rooms[/]:");
        AnsiConsole.Markup("apartmet Type \n" +
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
            Apartment created = await apartmentService.Create(model);

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
    public async Task CheckApartmentsForStatus()
    {
        apartmentService.
    }
    public async Task HotelBalance()
    {

    }
    public async Task UpdateAdminPassword()
    {
        
    }
}
