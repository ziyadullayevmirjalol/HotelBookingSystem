using Newtonsoft.Json;

namespace HotelBookingSystem.Models;

public class Admin
{
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("hotel_balance_info")]
    public double HotelBalanceInfo { get; set; }
}
