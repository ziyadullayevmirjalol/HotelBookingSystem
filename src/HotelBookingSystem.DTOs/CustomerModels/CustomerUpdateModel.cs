using HotelBookingSystem.Domain.Entities.Commons;
using Newtonsoft.Json;

namespace HotelBookingSystem.DTOs.CustomerModels;

public class CustomerUpdateModel : Auditable
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
}