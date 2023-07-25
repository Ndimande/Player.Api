using Microsoft.EntityFrameworkCore;
using Player.Api.Models;
using System.Text.RegularExpressions;

namespace Player.Api.Repository.SeedTransactionTypes
{
    public static class TransactionModelBuilderExtention
    {
        public static void AddTransaction(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasData(
                       new Transaction
                       {
                           id = 1,
                           balance= 0.0,
                           LastUpdatedOn = DateTime.UtcNow,
                           playerId= 1,
                           transactionTypesId = 2,
                           updatedBy = "Patrick"
                           
                       }
                   );
        }
    }
}

