using Player.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using Player.Api.Repository.SeedTransactionTypes;

namespace Player.Api.Repository;

/// <summary>
///     The <see cref="DbContext" /> OldMutualContext class
/// </summary>
public class PlayerDBContext : DbContext
{
    /// <summary>
    ///     Instantiates a <see cref="DbContext" /> context
    /// </summary>
    /// <param name="options">DbContext options</param>
    public PlayerDBContext(DbContextOptions<PlayerDBContext> options)
        : base(options)
    {}


    #region DbSet properties

    /// <summary>
    ///     Get or set the <see cref="DbSet{Player" /> Player DbSet
    /// </summary>
    public DbSet<Models.Player> Players { get; set; } = null!;

    /// <summary>
    ///     Get or set the <see cref="DbSet{Player" /> Transaction DbSet
    /// </summary>
    public DbSet<Transaction> Transaction { get; set; } = null!;


    /// <summary>
    ///     Get or set the <see cref="DbSet{Player" /> TransactionTypes DbSet
    /// </summary>
    public DbSet<Models.TransactionTypes> TransactionTypes { get; set; } = null!;

    /// <summary>
    ///     Get or set the <see cref="DbSet{Player" /> TransactionAuditTrail DbSet
    /// </summary>
    public DbSet<Models.TransactionAuditTrail> TransactionAuditTrail { get; set; } = null!; //The purpose of this table is to track all in the transaction table


    #endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddTransactionType();
        modelBuilder.AddPlayers();
        modelBuilder.AddTransaction();
    }
}
