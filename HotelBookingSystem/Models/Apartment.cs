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
    [JsonProperty("RoomType")]
    public ApartmentType ApartmentType { get; set; }
}
