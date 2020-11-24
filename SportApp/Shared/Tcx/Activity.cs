using System.Collections.Generic;

public class Activity
{
    public int Id { set; get; }
    public int UserId { set; get; }
    public string Sport { set; get; }
    public List<Lap> Laps { set; get; }
}