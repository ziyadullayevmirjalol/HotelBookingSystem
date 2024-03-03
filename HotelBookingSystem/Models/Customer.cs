
using HotelBookingSystem.Models.Commons;
using Newtonsoft.Json;

namespace HotelBookingSystem.Models;

public class Customer : Auditable
{
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("firstname")]
    public string Firstname { get; set; }
    [JsonProperty("lastname")]
    public string Lastname { get; set; }
    [JsonProperty("balance")]
    public double Balance { get; set; }
    [JsonProperty("room_id")]
    public int ApartmentId { get; set; }
}
