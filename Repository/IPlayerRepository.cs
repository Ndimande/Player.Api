

using Player.Api.Exceptions;
using Player.Api.Models;

namespace Player.Api.Repository;

/// <summary>
///     A Data Access Layer (DAL) interface for <see cref="Player" /> Player objects
/// </summary>
public interface IPlayerRepository
{

    /// <summary>
    ///     Updates a <see cref="Player" /> Player object
    /// </summary>
    /// <param name=""><see cref="Player" />Player </param>
    /// <returns>Http Request Response</returns>
    double? GetPlayerBalanceAsync(int playerId);

    /// <summary>
    ///     Updates a <see cref="Transaction" /> Transaction object
    /// </summary>
    /// <param name="PlayerId"><see cref="Transaction" />PlayerId </param>
    ///  <param name="value"><see cref="Transaction" />value </param>
    /// <returns>Http Request Response</returns>
    Task DebitPlayerAsync(int playerId, double value, string? note);

    /// <summary>
    ///     Updates a <see cref="Transaction" /> Transaction object
    /// </summary>
    /// <param name="PlayerId"><see cref="Transaction" />PlayerId </param>
    ///  <param name="value"><see cref="Transaction" />value </param>
    /// <returns>Http Request Response</returns>
    Task CreditPlayerAsync(int playerId, double value, string? note);

    /// <summary>
    ///     Updates a <see cref="Transaction" /> Transaction object
    /// </summary>
    /// <param name="PlayerId"><see cref="Transaction" />PlayerId </param>
    ///  <param name="value"><see cref="Transaction" />value </param>
    /// <returns>Http Request Response</returns>
    Task RefundPlayerAsync(int playerId, double value, string? note);

    /// <summary>
    ///     Check if <see cref="DbSet{Player}" /> Player Dbset is null
    /// </summary>
    /// <returns>True if null</returns>
    bool IsPlayerNull();

}