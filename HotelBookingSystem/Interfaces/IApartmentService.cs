using HotelBookingSystem.Models;

namespace HotelBookingSystem.Interfaces;

internal interface IApartmentService
{
    public ValueTask<Apartment> Create(ApartmentCreateModel apartment);
    public ValueTask<bool> Delete(int id);
    public ValueTask<Apartment> Get(int id);
    public ValueTask<List<Apartment>> GetAll();
    public ValueTask<bool> SetOrdered(int apartmentId, int customerId);
    public ValueTask<bool> SetUnordered(int apartmentId, int customerId);
    public ValueTask<List<Apartment>> OrderedApartments();
    public ValueTask<List<Apartment>> NotOrderedApartments();
}
