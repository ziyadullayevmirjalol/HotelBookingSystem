using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using HotelBookingSystem.SpectreUI.CustomerPanel.Actions;
using Spectre.Console;

namespace HotelBookingSystem.SpectreUI.CustomerPanel;

public class CustomerMenu
{
    private Customer Customer;
    private CustomerActions customerActions;
    private CustomerService customerService;
    public CustomerMenu(Customer customer, CustomerService customerService)
    {
        this.Customer = customer;
        this.customerService = customerService;
        customerActions = new CustomerActions(customer, customerService);
    }
    #region Menu
    public async Task Menu()
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
                    await customerActions.Deposit();
                    break;
                case "Book new apartment":
                    AnsiConsole.Clear();
                    await customerActions.BookNewApartment();
                    break;
                case "Remove already booked apartment":
                    AnsiConsole.Clear();
                    await customerActions.RemoveBookedApartment();
                    break;
                case "View Profile":
                    AnsiConsole.Clear();
                    await customerActions.ViewProfile();
                    break;
                case "Update customer details":
                    AnsiConsole.Clear();
                    await customerActions.UpdateCustomerDetails();
                    break;
                case "Delete account\n":
                    AnsiConsole.Clear();
                    await customerActions.DeleteAccount();
                    return;
                case "[red]Sign out[/]":
                    return;
            }
        }
    }
    #endregion
}
