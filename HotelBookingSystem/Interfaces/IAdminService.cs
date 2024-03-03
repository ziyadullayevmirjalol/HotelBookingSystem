using HotelBookingSystem.Models;

namespace HotelBookingSystem.Interfaces;

public interface IAdminService
{
    public ValueTask<Apartment> AddNewApartmentAsync(ApartmentCreateModel newApartment);
    public ValueTask<Apartment> GetApartmentByIdAsync(int id);
    public ValueTask<List<Apartment>> GetAllApartmentsAsync();
    public ValueTask<List<Customer>> GetAllCustomersAsync();
    public ValueTask<Admin> UpdatePasswordAsync(Admin newAdmin);
    public ValueTask<Admin> LoginAsAdminAsync(string password);
    public ValueTask<double> HotelBalanceInfoAsync();
}