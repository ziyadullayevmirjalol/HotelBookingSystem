﻿using HotelBookingSystem.Models;

namespace HotelBookingSystem.Interfaces;

public interface ICustomerService
{
    public ValueTask<CustomerViewModel> Create(CustomerCreateModel customer);
    public ValueTask<CustomerViewModel> Update(int customerId, CustomerUpdateModel customer);
    public ValueTask<bool> Delete(int customerId);
    public ValueTask<CustomerViewModel> ViewCustomer(int customerId);
    public ValueTask<Customer> GetCustomer(int customerId);
    public ValueTask<Customer> GetToLogin(string username, string password);
    public ValueTask<List<Customer>> GetAll();
    public ValueTask<bool> BookApartment(int apartmentId, int customerId);
    public ValueTask<bool> DeleteBookingApartment(int apartmentId, int customerId);
}