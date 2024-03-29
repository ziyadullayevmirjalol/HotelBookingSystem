﻿using HotelBookingSystem.Services.Services;
using Spectre.Console;

namespace HotelBookingSystem.Presentation.SpectreUI.AdminPanel;

public class AdminLogin
{
    private AdminService adminService;
    private ApartmentService apartmentService;
    private AdminMenu adminMenu;

    public AdminLogin(AdminService adminService, ApartmentService apartmentService)
    {
        this.adminService = adminService;
        this.apartmentService = apartmentService;
    }

    #region Login
    public async Task LoginAsync()
    {
        AnsiConsole.Clear();
        while (true)
        {
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]password[/]:")
            .PromptStyle("yellow")
            .Secret());

            try
            {
                var getAdmin = await adminService.LoginAsAdminAsync(password);

                adminMenu = new AdminMenu(getAdmin, adminService, apartmentService);
                await adminMenu.MenuAsync();
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
