using System.Text.Json.Serialization;

namespace TicimaxTurnike.Entity;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsDisabled { get; set; }

    [JsonIgnore]
    public List<Entry> Entries { get; set; }
    
    [JsonIgnore]
    public List<LastEntryDetail> LastEntryDetails { get; set; }

}