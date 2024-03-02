using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using HotelBookingSystem.SpectreUI.AdminPanel.Actions;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.AdminPanel;

public class AdminMenu
{
    private Admin admin;
    private AdminActions adminActions;
    private AdminService adminService;
    private ApartmentService apartmentService;
    public AdminMenu(Admin admin, AdminService adminService, ApartmentService apartmentService)
    {
        this.admin = admin;
        this.adminService = adminService;
        this.apartmentService = apartmentService;
        adminActions = new AdminActions(admin, adminService, apartmentService);
    }
    public async Task Menu()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Dream[green]House[/][red]/[/]Admin")
                    .PageSize(4)
                    .AddChoices(new[] {
                        "Add new apartment to the Hotel",
                        "Check Apartments status",
                        "View summary balance of Hotel",
                        "Update admin password\n",
                        "[red]Sign out[/]"}));

            switch (choise)
            {
                case "Add new apartment to the Hotel":
                    AnsiConsole.Clear();
                    await adminActions.AddNewApartment();
                    break;
                case "Check Apartments status":
                    AnsiConsole.Clear();
                    await adminActions.CheckApartmentsForStatus();
                    break;
                case "View summary balance of Hotel":
                    AnsiConsole.Clear();
                    await adminActions.HotelBalance();
                    break;
                case "Update admin password\n":
                    AnsiConsole.Clear();
                    await adminActions.UpdateAdminPassword();
                    break;
                case "[red]Sign out[/]":
                    return;
            }
        }
    }
}
