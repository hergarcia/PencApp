namespace PencApp.Models;

public class User
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Photo { get; set; }
    public DateTime DateOfBirth { get; set; } = new(1994, 10, 14);
    public bool Activated { get; set; }
    public bool RememberMe { get; set; } = true;
    
    public string DisplayName => FirstName + " " + LastName;
}