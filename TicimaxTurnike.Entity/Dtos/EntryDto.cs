using System.Text.Json.Serialization;

namespace TicimaxTurnike.Entity.Dtos;

public class EntryDto
{
    
    [JsonIgnore]
   public DateTime Date { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public string Type { get; set; } = "Enter";
    
    public int PersonId { get; set; }
    
}