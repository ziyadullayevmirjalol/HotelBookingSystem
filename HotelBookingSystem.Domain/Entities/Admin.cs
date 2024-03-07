using Newtonsoft.Json;

namespace HotelBookingSystem.Domain.Entities;

public class Admin
{
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("hotel_balance_info")]
    public double HotelBalanceInfo { get; set; }
}
