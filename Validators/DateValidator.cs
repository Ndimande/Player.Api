namespace Player.Api.Validators;

/// <summary>
///     The DateValidator class contains methods for validating various date format fields
/// </summary>
public class DateValidator
{
    /// <summary>
    ///     Check if a string is a valid date
    /// </summary>
    /// <param name="dateString">A <see cref="string" /> representing a date</param>
    /// <returns>True, if the date is valid</returns>
    public bool IsValidDate(string dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString)) return false;
        DateTime date;
        var result = DateTime.TryParse(dateString, out date);
        return result;
    }
}