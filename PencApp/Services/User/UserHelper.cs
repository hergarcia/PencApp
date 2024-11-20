using PencApp.Services.ApiClient;

namespace PencApp.Services.User;

public static class UserHelper
{
    public static Models.User ToUser(this AdminUserDTO adminUserDTO)
    {
        return new Models.User()
        {
            Username = adminUserDTO.Login,
            FirstName = adminUserDTO.FirstName,
            LastName = adminUserDTO.LastName,
            EmailAddress = adminUserDTO.Email,
            Photo = adminUserDTO.ImageUrl
        };
    }

    public static AdminUserDTO ToAdminUserDTO(this Models.User user)
    {
        return new AdminUserDTO()
        {
            Login = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.EmailAddress,
            ImageUrl = user.Photo,
            Activated = user.Activated,
        };
    }
    
    public static ManagedUserVM ToManagedUserVM(this Models.User user)
    {
        return new ManagedUserVM()
        {
            Login = user.Username,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.EmailAddress,
            ImageUrl = user.Photo,
            Activated = user.Activated,
        };
    }
    
    public static LoginVM ToLoginVM(this Models.User user)
    {
        return new LoginVM()
        {
            Username = user.Username,
            Password = user.Password,
            RememberMe = user.RememberMe,
            
        };
    }
}