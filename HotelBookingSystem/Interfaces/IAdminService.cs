using HotelBookingSystem.Models;

namespace HotelBookingSystem.Interfaces;

public interface IAdminService
{
    public ValueTask<Apartment> AddNewApartment(ApartmentCreateModel newApartment);
    public ValueTask<Apartment> GetApartmentById(int id);
    public ValueTask<List<Apartment>> GetAllApartments();
    public ValueTask<List<Customer>> GetAllCustomers();
    public ValueTask<Admin> UpdatePassword(Admin newAdmin);
    public ValueTask<Admin> LoginAsAdmin(string password);
}