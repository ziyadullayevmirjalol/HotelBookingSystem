using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.DTOs.ApartmentModels;
using HotelBookingSystem.Services.Configurations;
using HotelBookingSystem.Services.Extensions;
using HotelBookingSystem.Services.Helpers;
using HotelBookingSystem.Services.Interfaces;

namespace HotelBookingSystem.Services.Services;

public class ApartmentService : IApartmentService
{
    private List<Apartment> apartments;

    public async ValueTask<Apartment> CreateAsync(ApartmentCreateModel apartment)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var createdApartment = apartments.Create(apartment.MapTo<Apartment>());

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);

        return createdApartment;
    }
    public async ValueTask<bool> DeleteAsync(int id)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == id && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {id}");

        existApartment.IsDeleted = true;
        existApartment.DeletedAt = DateTime.Now;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<Apartment> GetAsync(int id)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == id && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {id}");

        return existApartment;
    }
    public async ValueTask<List<Apartment>> GetAllAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);
        return apartments.Where(apartment => !apartment.IsDeleted).ToList();
    }
    public async ValueTask<bool> SetOrderedAsync(int apartmentId, int customerId)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == apartmentId && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {apartmentId}");
        existApartment.OrderedCustomerId = customerId;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<bool> SetUnorderedAsync(int apartmentId, int customerId)
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        var existApartment = apartments.FirstOrDefault(apartment => apartment.Id == apartmentId && apartment.OrderedCustomerId == customerId && !apartment.IsDeleted)
            ?? throw new Exception($"Apartment not found with id: {apartmentId}");
        existApartment.OrderedCustomerId = 0;

        await FileIO.WriteAsync(Constants.APARTMENTSPATH, apartments);
        return true;
    }
    public async ValueTask<List<Apartment>> BookedApartmentsAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.OrderedCustomerId > 0).ToList();
    }
    public async ValueTask<List<Apartment>> NotBookedApartmentsAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.OrderedCustomerId == 0).ToList();
    }
    public async ValueTask<List<Apartment>> GetAllPremiumAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.ApartmentType == Domain.Enums.ApartmentType.Econo).ToList();
    }
    public async ValueTask<List<Apartment>> GetAllNormalAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.ApartmentType == Domain.Enums.ApartmentType.Normal).ToList();
    }
    public async ValueTask<List<Apartment>> GetAllPreminumAsync()
    {
        apartments = await FileIO.ReadAsync<Apartment>(Constants.APARTMENTSPATH);

        return apartments.Where(a => a.ApartmentType == Domain.Enums.ApartmentType.Premium).ToList();
    }
}
