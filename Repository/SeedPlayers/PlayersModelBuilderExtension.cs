
using Microsoft.EntityFrameworkCore;
using Player.Api.Models;
using System.Text.RegularExpressions;

namespace Player.Api.Repository.SeedTransactionTypes
{
    public static class PlayersModelBuilderExtension
    {
        public static void AddPlayers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Player>().HasData(
                       new Models.Player
                       {
                           id = 1,
                           name = "Patrick",
                           surname = "Ndimande",
                           username = "Ndimanden",
                           passwordHash = new byte[] { },
                           passwordSalt = new byte[] { }
                           // = "test"


                       },
                       new Models.Player
                       {
                           id = 2,
                           name = "Nqobani",
                           surname = "Ndimande",
                           username = "Ndimanden",
                           passwordHash = new byte[] {},
                           passwordSalt = new byte[] {}
                           
                         //  password = "test"
                       }

                   );
        }
    }
}

