using HotelBookingSystem.Domain.Entities.Commons;
using HotelBookingSystem.Domain.Enums;
using Newtonsoft.Json;

namespace HotelBookingSystem.Domain.Entities;

public class Apartment : Auditable
{
    [JsonProperty("price")]
    public double Price { get; set; }
    [JsonProperty("count_of_rooms")]
    public int CountOfRooms { get; set; }
    [JsonProperty("room_type")]
    public ApartmentType ApartmentType { get; set; }
    [JsonProperty("ordered_customer_id")]
    public int OrderedCustomerId { get; set; }
}
