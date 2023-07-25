
using Player.Api.Exceptions;
using Player.Api.Models;

namespace Player.Api.Services;

/// <summary>
///     A Data Access Layer (DAL) interface for <see cref="Player" /> Player objects
/// </summary>
public interface IPlayerServices
{
    /// <summary>
    ///     Check if <see cref="DbSet{Models.Player}" />GetPlayerBalanceAsync
    /// </summary>
    /// <returns>True if null</returns>
    double? GetPlayerBalance(int playerId);


    /// <summary>
    ///     Check if <see cref="DbSet{Transaction}" />Transaction
    /// </summary>
    /// <returns>True if null</returns>
    Task DebitPlayerAsync(int playerId,double value, string? note);


    /// <summary>
    ///     Check if <see cref="DbSet{Transaction}" />Transaction
    /// </summary>
    /// <returns>True if null</returns>
    Task CreditPlayerAsync(int playerId, double value, string? note);


    /// <summary>
    ///     Check if <see cref="DbSet{Transaction}" />Transaction
    /// </summary>
    /// <returns>True if null</returns>
    Task RefundPlayerAsync(int playerId, double value, string? note);

    /// <summary>
    ///     Check if <see cref="DbSet{Player}" /> Player Dbset is null
    /// </summary>
    /// <returns>True if null</returns>
    bool IsUserNull();

}