using HotelBookingSystem.Domain.Entities.Commons;
using HotelBookingSystem.Domain.Enums;
using Newtonsoft.Json;

namespace HotelBookingSystem.DTOs.ApartmentModels;

public class ApartmentCreateModel : Auditable
{
    [JsonProperty("price")]
    public double Price { get; set; }
    [JsonProperty("count_of_rooms")]
    public int CountOfRooms { get; set; }
    [JsonProperty("RoomType")]
    public ApartmentType ApartmentType { get; set; }
}
