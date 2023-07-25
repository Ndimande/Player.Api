using System.ComponentModel.DataAnnotations;

namespace Player.Api.Models;

public class TransactionTypes
{
    /// <summary>
    ///     Get or set the <see cref="int" /> primary Id property
    /// </summary>
    [Required]
    public int id { get; set; }

    /// <summary>
    ///     Get or set the <see cref="string" /> username property
    /// </summary>
    public string? transationName { get; set; }

    /// <summary>
    ///     Get or set the <see cref="string" /> notes property
    /// </summary>
    public string? notes { get; set; } 

}
