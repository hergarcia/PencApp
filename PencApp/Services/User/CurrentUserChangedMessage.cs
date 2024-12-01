using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PencApp.Services.User;


public class CurrentUserChangedMessage : ValueChangedMessage<Models.User?> 
{
    public CurrentUserChangedMessage(Models.User? value) : base(value)
    {
    }
}