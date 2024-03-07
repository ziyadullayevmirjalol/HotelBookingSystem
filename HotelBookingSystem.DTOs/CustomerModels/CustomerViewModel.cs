using Newtonsoft.Json;

namespace HotelBookingSystem.DTOs.CustomerModels;

public class CustomerViewModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("balance")]
    public double Balance { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("firstname")]
    public string Firstname { get; set; }
    [JsonProperty("lastname")]
    public string Lastname { get; set; }
    [JsonProperty("apartment_id")]
    public int ApartmentId { get; set; }
}