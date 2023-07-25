using System.ComponentModel.DataAnnotations;

namespace Player.Api.Models;

public class Player
{
    /// <summary>
    ///     Get or set the <see cref="int" /> primary Id property
    /// </summary>
    [Required]
    public int id { get; set; }

    /// <summary>
    ///     Get or set the <see cref="string" /> username property
    /// </summary>
    public string? name { get; set; }


    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    public string? surname { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> username property
    /// </summary>
    public string? username { get; set; }

    /// <summary>
    ///     Get or set the <see cref="Byte" /> password property
    /// </summary>
    public byte[] passwordHash { get; set; }

    /// <summary>
    ///     Get or set the <see cref="Byte" /> password property
    /// </summary>
    public byte[] passwordSalt { get; set; }

}


public class UserDto
{
    /// <summary>
    ///     Get or set the <see cref="int" /> primary Id property
    /// </summary>
    [Required]
    public int id { get; set; }

    /// <summary>
    ///     Get or set the <see cref="string" /> username property
    /// </summary>
    public string? username { get; set; }


    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    public string? password { get; set; }


}