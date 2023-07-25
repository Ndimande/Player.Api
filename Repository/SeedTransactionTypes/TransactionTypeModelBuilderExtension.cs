using Microsoft.EntityFrameworkCore;
using Player.Api.Models;
using System.Text.RegularExpressions;

namespace Player.Api.Repository.SeedTransactionTypes
{
    public static class TransactionTypeModelBuilderExtension
    {
        public static void AddTransactionType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionTypes>().HasData(
                       new TransactionTypes
                       {
                           id = 1,
                           transationName = "Debit Player"
                       },
                       new TransactionTypes
                        {
                            id = 2,
                            transationName = "Credit Player"
                        },
                       new TransactionTypes
                        {
                            id = 3,
                            transationName = "Refund Player"    
                        }
                   );
        }
    }
}

