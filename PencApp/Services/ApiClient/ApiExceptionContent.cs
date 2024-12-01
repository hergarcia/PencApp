namespace PencApp.Services.ApiClient;

public class ApiExceptionContent
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
    public string? Message { get; set; }
    public string? Path { get; set; }

    public override string ToString()
    {
        return "Type: " + Type 
            + ",\nTitle: " + Title 
            + ",\nStatus: " + Status 
            + ",\nDetail: " + Detail 
            + ",\nMessage: " + Message 
            + ",\nPath: " + Path;
    }
}