using Newtonsoft.Json;

namespace HotelBookingSystem.Domain.Entities.Commons;
public class Auditable
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
    [JsonProperty("deleted_at")]
    public DateTime DeletedAt { get; set; }
    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }
}
