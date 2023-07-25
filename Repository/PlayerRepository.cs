

using Player.Api.Exceptions;
using Player.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security;
using System.Security.Permissions;

namespace Player.Api.Repository;

/// <summary>
///     Provides a Data Access Layer (DAL) for manipulating the <see cref="Dbset{Player}" /> Player Entity
/// </summary>
public class PlayerRepository : IPlayerRepository
{
    /// <summary>
    ///     The <see cref="PlayerDBContext" /> PlayerDBContext context
    /// </summary>
    private readonly PlayerDBContext _context;

    /// <summary>
    ///     Instatiates the PfaAreasRepository
    /// </summary>
    /// <param name="context"><see cref="PlayerDBContext" /> PlayerDBContext context</param>
    public PlayerRepository(PlayerDBContext context)
    {
        _context = context;
    }

    /// <inheritdoc cref="GetPlayerBalanceAsync" />
    public double? GetPlayerBalanceAsync(int playerId)
    {
        return _context.Transaction.Where(tr => tr.playerId.Equals(playerId)).FirstOrDefault()?.balance;
    }

    /// <inheritdoc cref="DebitPlayerAsync" />
    public async Task DebitPlayerAsync(int playerId,double value, string? note)
    {
        var transaction = _context.Transaction.Where(tr => tr.playerId.Equals(playerId)).FirstOrDefault() ;
        if (transaction  != null)
        {

            try
            {
                
                transaction.balance -= value; //Debitting
                transaction.LastUpdatedOn = DateTime.UtcNow;
                transaction.notes = note;
                transaction.transactionTypesId = 1; //Type one is Debit
                await WriteToTransactionAuditTrail(transaction, value);
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex) {
                Logging.WriteErrorLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

    }

    /// <inheritdoc cref="CreditPlayerAsync" />
    public async Task CreditPlayerAsync(int playerId, double value, string? note)
    {
        var transaction = _context.Transaction.Where(tr => tr.playerId.Equals(playerId)).FirstOrDefault();
        if (transaction != null)
        {

            try
            {
                transaction.balance += value; //Debitting
                transaction.LastUpdatedOn = DateTime.UtcNow;
                transaction.notes = note;
                transaction.transactionTypesId = 2; //Type one is Credit
                await WriteToTransactionAuditTrail(transaction, value);
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Logging.WriteErrorLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

    }

    /// <inheritdoc cref="RefundPlayerAsync" />
    public async Task RefundPlayerAsync(int playerId, double value, string? note)
    {
        var transaction = _context.Transaction.Where(tr => tr.playerId.Equals(playerId)).FirstOrDefault();
        if (transaction != null)
        {

            try
            {
                transaction.balance += value; //Debitting
                transaction.LastUpdatedOn = DateTime.UtcNow;
                transaction.notes = note;
                transaction.transactionTypesId = 3; //Type three is Refund
                await WriteToTransactionAuditTrail(transaction, value);
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Logging.WriteErrorLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

    }

    /// <inheritdoc cref="Player" />
    public bool UserExists(int id)
    {
        return (_context.Players?.Any(e => e.id == id)).GetValueOrDefault();
    }

    /// <inheritdoc cref="IsPlayerNull" />
    public bool IsPlayerNull()
    {
        return _context.Players == null ? true : false;
    }

    /// <inheritdoc cref="RefundPlayerAsync" />
    public async Task WriteToTransactionAuditTrail(Transaction transaction, double value)
    {
            try
            {
            await _context.TransactionAuditTrail.AddAsync(new TransactionAuditTrail()
            {
                transactionTypesId = transaction.transactionTypesId,
                value = value,
                balance = transaction.balance,
                LastUpdatedOn = DateTime.UtcNow,
                notes = transaction.notes,
                playerId = transaction.playerId,
                updatedBy = transaction.updatedBy,
            }); ;;
            await _context.SaveChangesAsync();

        }
            catch (Exception ex)
            {
                Logging.WriteErrorLog(ex.Message);
                Console.WriteLine(ex.Message);
            }
    }

}