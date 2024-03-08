using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.DTOs.ApartmentModels;

namespace HotelBookingSystem.Services.Interfaces;

internal interface IApartmentService
{
    public ValueTask<Apartment> CreateAsync(ApartmentCreateModel apartment);
    public ValueTask<bool> DeleteAsync(int id);
    public ValueTask<Apartment> GetAsync(int id);
    public ValueTask<List<Apartment>> GetAllAsync();
    public ValueTask<bool> SetOrderedAsync(int apartmentId, int customerId);
    public ValueTask<bool> SetUnorderedAsync(int apartmentId, int customerId);
    public ValueTask<List<Apartment>> BookedApartmentsAsync();
    public ValueTask<List<Apartment>> NotBookedApartmentsAsync();
    public ValueTask<List<Apartment>> GetAllPremiumAsync();
    public ValueTask<List<Apartment>> GetAllNormalAsync();
    public ValueTask<List<Apartment>> GetAllPreminumAsync();

}
