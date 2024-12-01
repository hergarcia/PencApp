using System.Windows.Input;

namespace PencApp.Models;

public class SimpleError
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string Icon { get; set; } = "alert_circle";
    public string ButtonText { get; set; }
    public ICommand? ButtonCommand { get; set; }
}