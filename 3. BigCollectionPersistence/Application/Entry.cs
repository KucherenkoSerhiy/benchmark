namespace Application;

public class Entry
{
    public int Id { get; set; }

    public string Data { get; }

    public Entry(string data)
    {
        Data = data;
    }
}