using HotelBookingSystem.Configurations;
using HotelBookingSystem.Extensions;
using HotelBookingSystem.Helpers;
using HotelBookingSystem.Interfaces;
using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services;

public class ApartmentService : IApartmentService
{
    private List<Apartment> apartments;

    public async ValueTask<Apartment> Create(ApartmentCreateModel apartment)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var createdApartment = apartments.Create(apartment.MapTo<Apartment>());

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);

        return createdApartment;
    }
    public async ValueTask<bool> Delete(int id)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == id && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {id}");

        existApartment.IsDeleted = true;
        existApartment.DeletedAt = DateTime.Now;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<Apartment> Get(int id)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == id && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {id}");

        return existApartment;
    }
    public async ValueTask<List<Apartment>> GetAll()
    {
        return await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);
    }
    public async ValueTask<bool> SetOrdered(int apartmentId, int customerId)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == apartmentId && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {apartmentId}");
        existApartment.OrderedCustomerId = customerId;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<bool> SetUnordered(int apartmentId, int customerId)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == apartmentId && apartment.OrderedCustomerId == customerId && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {apartmentId}");
        existApartment.OrderedCustomerId = 0;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<List<Apartment>> OrderedApartments()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.OrderedCustomerId > 0).ToList();
    }
    public async ValueTask<List<Apartment>> NotOrderedApartments()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.OrderedCustomerId == 0).ToList();
    }
}
