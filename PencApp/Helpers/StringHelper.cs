using System.Text.RegularExpressions;

namespace PencApp.Helpers;

public static class StringHelper
{
    public static bool IsEmailValid(string email)
    {
        const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }
}