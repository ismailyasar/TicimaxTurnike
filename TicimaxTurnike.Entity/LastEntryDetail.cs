using System.Text.Json.Serialization;

namespace TicimaxTurnike.Entity;

public class LastEntryDetail
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Day  { get; set; }
    
    public DateTime Date { get; set; }

    public string Type { get; set; }    
    
    [JsonIgnore]
    public Person Person { get; set; }
}