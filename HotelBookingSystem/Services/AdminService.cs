using HotelBookingSystem.Configurations;
using HotelBookingSystem.Helpers;
using HotelBookingSystem.Interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services;

public class AdminService : IAdminService
{
    private List<Admin> administration;
    private ApartmentService apartmentService;
    private CustomerService customerService;

    public AdminService(ApartmentService apartmentService, CustomerService customerService)
    {
        this.apartmentService = apartmentService;
        this.customerService = customerService;
    }
    public async ValueTask<Apartment> AddNewApartment(ApartmentCreateModel newApartment)
    {
        return await apartmentService.Create(newApartment);
    }
    public async ValueTask<List<Apartment>> GetAllApartments()
    {
        return await apartmentService.GetAll();
    }
    public async ValueTask<List<Customer>> GetAllCustomers()
    {
        return await customerService.GetAll();
    }
    public async ValueTask<Apartment> GetApartmentById(int id)
    {
        return await apartmentService.Get(id);
    }
    public async ValueTask<Admin> LoginAsAdmin(string password)
    {
        administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

        var admin = administration.FirstOrDefault(a => a.Password == password)
            ?? throw new Exception("Password is not match.");
        return admin;
    }
    public async ValueTask<Admin> UpdatePassword(Admin newAdmin)
    {
        administration = await FileIO.ReadAsync<Admin>(Constants.ADMINISTRATIONINFO);

        var admin = administration.FirstOrDefault()
            ?? throw new Exception("administration info corrupted.");
        admin.Password = newAdmin.Password;

        await FileIO.WriteAsync(Constants.ADMINISTRATIONINFO, administration);
        return admin;
    }
}
