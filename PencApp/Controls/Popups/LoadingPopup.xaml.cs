using CommunityToolkit.Maui.Views;
using PencApp.Resources.Languages;

namespace PencApp.Controls.Popups;

public partial class LoadingPopup : Popup
{
    private string _loadingText;
    private bool _isTextIncreasing = true;
    public LoadingPopup(string? text)
    {
        Opened += async (sender, args) => await AnimateText();
        InitializeComponent();
        _loadingText = text ?? AppResources.Loading;
    }
    
    private async Task AnimateText()
    {
        while (true)
        {
            var labelText = string.IsNullOrEmpty(Label.Text) 
                ? _loadingText[0].ToString() 
                : Label.Text;
            
            if (_isTextIncreasing && labelText.Length < _loadingText.Length)
            {   
                labelText = _loadingText.Substring(0, labelText.Length + 1);            
                await Task.Delay(TimeSpan.FromMilliseconds(50));
            }
            else if ((_isTextIncreasing && labelText.Length == _loadingText.Length) || labelText.Length == 1)
            {
                _isTextIncreasing = !_isTextIncreasing;
                
                if(!_isTextIncreasing)
                    await Task.Delay(TimeSpan.FromSeconds(1));
            }
            else
            {
                labelText = _loadingText.Substring(0, labelText.Length - 1);
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            
            Label.Text = labelText;
        }
    }
}