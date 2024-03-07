using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.DTOs.CustomerModels;

namespace HotelBookingSystem.Services.Interfaces;

public interface ICustomerService
{
    public ValueTask<CustomerViewModel> CreateAsync(CustomerCreateModel customer);
    public ValueTask<CustomerViewModel> UpdateAsync(int customerId, CustomerUpdateModel customer);
    public ValueTask<bool> DeleteAsync(int customerId);
    public ValueTask<CustomerViewModel> ViewCustomerAsync(int customerId);
    public ValueTask<Customer> GetCustomerAsync(int customerId);
    public ValueTask<Customer> GetToLoginAsync(string username, string password);
    public ValueTask<List<Customer>> GetAllAsync();
    public ValueTask<Customer> BookApartmentAsync(int apartmentId, int customerId);
    public ValueTask<Customer> DeleteBookingApartmentAsync(int apartmentId, int customerId);
    public ValueTask<Customer> DepositAsync(int customerId, double amount);
}