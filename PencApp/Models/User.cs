namespace PencApp.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDay { get; set; } = DateTime.Now;
    public bool Activated { get; set; }
    public bool RememberMe { get; set; } = true;
    
    public string DisplayName => FirstName + " " + LastName;
}