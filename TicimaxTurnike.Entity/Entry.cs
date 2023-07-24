namespace TicimaxTurnike.Entity;

public class Entry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
}