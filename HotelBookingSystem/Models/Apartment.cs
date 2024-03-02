using HotelBookingSystem.Enums;
using HotelBookingSystem.Models.Commons;
using Newtonsoft.Json;

namespace HotelBookingSystem.Models;

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
