using HotelBookingSystem.Configurations;
using HotelBookingSystem.Helpers;
using HotelBookingSystem.Interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services;

public class AdminService : IAdminService
{
    public AdminService(ApartmentService apartmentService, CustomerService customerService)
    {
        this.apartmentService = apartmentService;
        this.customerService = customerService;
    }

    private List<Admin> administration;
    private ApartmentService apartmentService;
    private CustomerService customerService;

    public async ValueTask<Apartment> AddNewApartmentAsync(ApartmentCreateModel newApartment)
    {
        return await apartmentService.CreateAsync(newApartment);
    }
    public async ValueTask<List<Apartment>> GetAllApartmentsAsync()
    {
        return await apartmentService.GetAllAsync();
    }
    public async ValueTask<List<Customer>> GetAllCustomersAsync()
    {
        return await customerService.GetAllAsync();
    }
    public async ValueTask<Apartment> GetApartmentByIdAsync(int id)
    {
        return await apartmentService.GetAsync(id);
    }
    public async ValueTask<Admin> LoginAsAdminAsync(string password)
    {
        administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

        var admin = administration.FirstOrDefault(a => PasswordHashing.VerifyPassword(a.Password, password))
            ?? throw new Exception("Password is not match.");
        return admin;
    }
    public async ValueTask<Admin> UpdatePasswordAsync(Admin newAdmin)
    {
        administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

        var admin = administration.FirstOrDefault()
            ?? throw new Exception("administration info corrupted.");

        admin.Password = PasswordHashing.Hashing(newAdmin.Password);

        await FileIO.WriteAsync(Constants.ADMINISTRATIONINFO, administration);
        return admin;
    }
    public async ValueTask<double> HotelBalanceInfoAsync()
    {
        administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

        var admin = administration.FirstOrDefault()
            ?? throw new Exception("administration info corrupted.");
        return admin.HotelBalanceInfo;
    }
}
