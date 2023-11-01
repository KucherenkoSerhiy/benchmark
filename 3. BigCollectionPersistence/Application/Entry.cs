namespace Application;

public class Entry
{
    public int Id { get; set; }

    public string Data { get; }

    private Entry() { } // EF Core uses this parameterless constructor.
    
    public Entry(string data)
    {
        Data = data;
    }
}