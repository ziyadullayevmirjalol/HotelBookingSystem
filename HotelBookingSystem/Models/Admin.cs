
using HotelBookingSystem.Models.Commons;
using Newtonsoft.Json;

namespace HotelBookingSystem.Models;

public class Admin : Auditable
{
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}
