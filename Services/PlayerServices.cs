
using Player.Api.Exceptions;
using Player.Api.Models;
using Player.Api.Repository;

namespace Player.Api.Services;

/// <summary>
///     Provides a service for manipulating the <see cref="Dbset{Player}" /> Player Entity
/// </summary>
public class PlayerServices : IPlayerServices
{
    /// <summary>
    ///     The <see cref="PlayerDBContext" /> PlayerDBContext context
    /// </summary>
    private readonly IPlayerRepository _repository;

    /// <summary>
    ///     Instatiates the Player
    /// </summary>
    /// <param name="repository">see <see cref="IPlayerRepository" /></param>
    public PlayerServices(IPlayerRepository repository)
    {
        _repository = repository;
    }

    public double? GetPlayerBalance(int playerId)
    {
        return _repository.GetPlayerBalanceAsync(playerId);
    }
    
    public async Task DebitPlayerAsync(int playerId,double value, string? note)
    {
        await _repository.DebitPlayerAsync(playerId,value,note);
    }

    public async Task CreditPlayerAsync(int playerId, double value, string? note)
    {
        await _repository.CreditPlayerAsync(playerId, value, note);
    }

    public async Task RefundPlayerAsync(int playerId, double value, string? note)
    {
        await _repository.RefundPlayerAsync(playerId, value, note);
    }
    /// <inheritdoc cref="IsUserNull" />
    public bool IsUserNull()
    {
        return _repository.IsPlayerNull();
    } 
}