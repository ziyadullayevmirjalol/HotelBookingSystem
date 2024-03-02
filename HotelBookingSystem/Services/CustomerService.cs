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

    List<Customer> customers;
    public async ValueTask<Customer> BookApartment(int apartmentId, int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        var existAparment = await apartmentService.Get(apartmentId)
            ?? throw new Exception($"Apartment is not exists with Id: {apartmentId}");

        if (existAparment.Price <= existCustomer.Balance)
        {
            existCustomer.Balance -= existAparment.Price;
            existCustomer.ApartmentId = apartmentId;
        }
        else
        {
            throw new Exception("Customer's balance is not enough to book this apartment");
        }

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer;

    }
    public async ValueTask<CustomerViewModel> Create(CustomerCreateModel customer)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        if (customers.Any(c => (c.Username == customer.Username || c.Email == customer.Email) && !c.IsDeleted))
            throw new Exception($"Customer is already exists with username: {customer.Username}.");

        var createdCustomer = customers.Create(customer.MapTo<Customer>());

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return createdCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<bool> Delete(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        existCustomer.IsDeleted = true;
        existCustomer.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return true;
    }
    public async ValueTask<Customer> DeleteBookingApartment(int apartmentId, int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customers => customers.Id == customerId && !customers.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");

        var existAparment = await apartmentService.Get(apartmentId)
            ?? throw new Exception($"Apartment is not exists with Id: {apartmentId}");

        existCustomer.ApartmentId = 0;

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer;
    }
    public async ValueTask<Customer> GetCustomer(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        return existCustomer;
    }
    public async ValueTask<Customer> GetToLogin(string username, string password)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Username == username && customer.Password == password && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with username or incorrect password: {username}");
        return existCustomer;
    }
    public async ValueTask<CustomerViewModel> Update(int customerId, CustomerUpdateModel customer)
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

        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<CustomerViewModel> ViewCustomer(int customerId)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);

        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        return existCustomer.MapTo<CustomerViewModel>();
    }
    public async ValueTask<List<Customer>> GetAll()
    {
        return await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);
    }
    public async ValueTask<Customer> Deposit(int customerId, double amount)
    {
        customers = await FileIO.ReadAsync<Customer>(Constants.CUSTOMERSPATH);
        var existCustomer = customers.FirstOrDefault(customer => customer.Id == customerId && !customer.IsDeleted)
            ?? throw new Exception($"Customer is not exists with Id: {customerId}");
        existCustomer.Balance += amount;
        await FileIO.WriteAsync(Constants.CUSTOMERSPATH, customers);
        return existCustomer;
    }
}
