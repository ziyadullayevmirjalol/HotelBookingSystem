using HotelBookingSystem.Configurations;
using HotelBookingSystem.Extensions;
using HotelBookingSystem.Helpers;
using HotelBookingSystem.Interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services;

public class CustomerService : ICustomerService
{
    private ApartmentService apartmentService;
    public CustomerService(ApartmentService apartmentService)
    {
        this.apartmentService = apartmentService;
    }

    private List<Customer> customers;

    public async ValueTask<Customer> BookApartmentAsync(int apartmentId, int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        var existAparment = await apartmentService.GetAsync(apartmentId)
            ?? throw new Exception($"Apartment is not exists with Id: {apartmentId}");

        if (existAparment.OrderedCustomerId == 0 && existCustomer.ApartmentId == 0)
        {
            if (existAparment.Price <= existCustomer.Balance)
            {
                var administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

                var admin = administration.FirstOrDefault()
                    ?? throw new Exception("administration info corrupted, transaction canceled.");

                existCustomer.Balance -= existAparment.Price;
                admin.HotelBalanceInfo += existAparment.Price;

                await FileIO.WriteAsync(Constants.ADMINISTRATIONINFO, administration);

                existCustomer.ApartmentId = apartmentId;

                await apartmentService.SetOrderedAsync(existAparment.Id, existCustomer.Id);
            }
            else
            {
                throw new Exception("Customer's balance is not enough to book this apartment");
            }
        }
        else if (existAparment.OrderedCustomerId == customerId)
        {
            throw new Exception("You are already booked this apartment");
        }
        else
        {
            throw new Exception("Someone has already booked this apartment or you have already booked another one.");
        }

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer;
    }
    public async ValueTask<CustomerViewModel> CreateAsync(CustomerCreateModel customer)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        if (customers.Any(c => (c.Username == customer.Username || c.Email == customer.Email) && !c.IsDeleted))
            throw new Exception($"Customer is already exists with username: {customer.Username}.");

        var createdCustomer = customers.Create(customer.MapTo<Customer>());

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return createdCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<bool> DeleteAsync(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        existCustomer.IsDeleted = true;
        existCustomer.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return true;
    }
    public async ValueTask<Customer> DeleteBookingApartmentAsync(int apartmentId, int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        var existAparment = await apartmentService.GetAsync(apartmentId)
            ?? throw new Exception($"Apartment is not exists with Id: {apartmentId}");

        try
        {
            existCustomer.ApartmentId = 0;
            await apartmentService.SetUnorderedAsync(apartmentId, customerId);
            await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return existCustomer;
    }
    public async ValueTask<Customer> GetCustomerAsync(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        return existCustomer;
    }
    public async ValueTask<Customer> GetToLoginAsync(string username, string password)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Username == username && customer.Password == password && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with username or incorrect password: {username}");
        return existCustomer;
    }
    public async ValueTask<CustomerViewModel> UpdateAsync(int customerId, CustomerUpdateModel customer)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        if (customers.Any(c => c.Id != customerId && c.Username == customer.Username))
            throw new Exception("Someone is using this username already");
        if (customers.Any(c => c.Id != customerId && c.Email == customer.Email))
            throw new Exception("Someone is using this email already");

        existCustomer.Username = customer.Username;
        existCustomer.Password = customer.Password;
        existCustomer.Email = customer.Email;
        existCustomer.Firstname = customer.Firstname;
        existCustomer.Lastname = customer.Lastname;
        existCustomer.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<CustomerViewModel> ViewCustomerAsync(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        return existCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<List<Customer>> GetAllAsync()
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);
        return customers.Where(customer => !customer.IsDeleted).ToList();
    }
    public async ValueTask<Customer> DepositAsync(int customerId, double amount)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);
        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        existCustomer.Balance += amount;
        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer;
    }
}
