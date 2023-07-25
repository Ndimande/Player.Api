

using System.Net.Mail;

namespace Player.Api.Validators;

/// <summary>
///     The EmailValidator class contains methods for validating email strings
/// </summary>
public class EmailValidator
{
    /// <summary>
    ///     Check if a string is a valid email
    /// </summary>
    /// <param name="emailString">A <see cref="string" /> representing an email</param>
    /// <returns>True, if the email is valid</returns>
    public bool IsValidEmail(string emailString)
    {
        if (string.IsNullOrWhiteSpace(emailString)) return false;
        try
        {
            var m = new MailAddress(emailString);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}