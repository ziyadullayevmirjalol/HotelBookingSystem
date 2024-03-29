﻿using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Presentation.SpectreUI.CustomerPanel.Actions;
using HotelBookingSystem.Services.Services;
using Spectre.Console;

namespace HotelBookingSystem.Presentation.SpectreUI.CustomerPanel;

public class CustomerMenu
{
    private Customer Customer;
    private CustomerActions customerActions;
    private CustomerService customerService;
    private ApartmentService apartmentService;
    public CustomerMenu(Customer customer, CustomerService customerService, ApartmentService apartmentService)
    {
        Customer = customer;
        this.customerService = customerService;
        this.apartmentService = apartmentService;
        customerActions = new CustomerActions(customer, customerService, apartmentService);
    }

    #region Menu
    public async Task MenuAsync()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choise = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                   .Title("Dream[green]House[/][red]/[/]Customer")
                   .PageSize(4)
                   .AddChoices(new[] {
                        "Deposit",
                        "View Booked Apartments",
                        "View not Booked Apartments",
                        "View All Econo class Apartments",
                        "View All Normal class Apartments",
                        "View All Premium class Apartments",
                        "Book new apartment",
                        "Remove already booked apartment",
                        "View Profile",
                        "Update customer details",
                        "Delete account\n",
                        "[red]Sign out[/]"}));

            switch (choise)
            {
                case "Deposit":
                    AnsiConsole.Clear();
                    await customerActions.DepositAsync();
                    break;
                case "View Booked Apartments":
                    AnsiConsole.Clear();
                    await customerActions.BookedApartmentsAsync();
                    break;
                case "View not Booked Apartments":
                    AnsiConsole.Clear();
                    await customerActions.NotBookedApartmentsAsync();
                    break;
                case "View All Econo class Apartments":
                    AnsiConsole.Clear();
                    await customerActions.GetAllEconoApartmentsAsync();
                    break;
                case "View All Normal class Apartments":
                    AnsiConsole.Clear();
                    await customerActions.GetAllNormalApartmentsAsync();
                    break;
                case "View All Premium class Apartments":
                    AnsiConsole.Clear();
                    await customerActions.GetAllPremiumApartmentsAsync();
                    break;
                case "Book new apartment":
                    AnsiConsole.Clear();
                    await customerActions.BookNewApartmentAsync();
                    break;
                case "Remove already booked apartment":
                    AnsiConsole.Clear();
                    await customerActions.RemoveBookedApartmentAsync();
                    break;
                case "View Profile":
                    AnsiConsole.Clear();
                    await customerActions.ViewProfileAsync();
                    break;
                case "Update customer details":
                    AnsiConsole.Clear();
                    await customerActions.UpdateCustomerDetailsAsync();
                    break;
                case "Delete account\n":
                    AnsiConsole.Clear();
                    await customerActions.DeleteAccountAsync();
                    return;
                case "[red]Sign out[/]":
                    return;
            }
        }
    }
    #endregion
}
