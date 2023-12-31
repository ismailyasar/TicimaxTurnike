using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicimaxTurnike.Entity;

public class Entry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    
    public int PersonId { get; set; }
    
    [JsonIgnore]
    public Person Person { get; set; }
}