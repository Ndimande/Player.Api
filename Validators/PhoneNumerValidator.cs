
using System.Text.RegularExpressions;

namespace Player.Api.Validators;

/// <summary>
///     The PhoneNumberValidator class contains methods for validating phone number strings
/// </summary>
public class PhoneNumberValidator
{
    /// <summary>
    ///     Check if a string is a valid local phone numeber
    /// </summary>
    /// <remarks>
    ///     A local phone number does not have a country code prefix
    ///     9999999
    ///     999 9999
    ///     999-9999
    ///     999 999 9999
    ///     999-999-9999
    /// </remarks>
    /// <param name="phoneNumberString">A <see cref="string" /> representing a phone number</param>
    /// <returns>True, if the phone number is valid</returns>
    public bool IsValidLocalPhoneNumber(string phoneNumberString)
    {
        if (string.IsNullOrWhiteSpace(phoneNumberString)) return false;

        var pattern1 = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        var test1 = Regex.IsMatch(phoneNumberString, pattern1);

        if (test1) return true;

        return false;
    }

    /// <summary>
    ///     Check if a string is a valid ine=ternational phone number
    /// </summary>
    /// <remarks>
    ///     An international phone number has a +99 (country specific) code prefix
    /// </remarks>
    /// <param name="phoneNumberString">A <see cref="string" /> representing a phone number</param>
    /// <returns>True, if the phone number is valid</returns>
    public bool IsValidInternationalPhoneNumber(string phoneNumberString)
    {
        if (string.IsNullOrWhiteSpace(phoneNumberString)) return false;

        var pattern1 = @"^(\+|00)[1-9][0-9 \-\(\)\.]{7,32}$";
        var test1 = Regex.IsMatch(phoneNumberString, pattern1);

        return test1;
    }
}