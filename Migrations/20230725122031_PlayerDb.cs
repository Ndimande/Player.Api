using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Player.Api.Migrations
{
    /// <inheritdoc />
    public partial class PlayerDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playerId = table.Column<int>(type: "int", nullable: false),
                    transactionTypesId = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionAuditTrail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playerId = table.Column<int>(type: "int", nullable: false),
                    transactionTypesId = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: true),
                    value = table.Column<double>(type: "float", nullable: true),
                    updatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAuditTrail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "id", "name", "passwordHash", "passwordSalt", "surname", "username" },
                values: new object[,]
                {
                    { 1, "Patrick", new byte[0], new byte[0], "Ndimande", "Ndimanden" },
                    { 2, "Nqobani", new byte[0], new byte[0], "Ndimande", "Ndimanden" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "id", "balance", "notes", "playerId", "transactionTypesId", "updatedBy" },
                values: new object[] { 1, 0.0, null, 1, 2, "Patrick" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "id", "notes", "transationName" },
                values: new object[,]
                {
                    { 1, null, "Debit Player" },
                    { 2, null, "Credit Player" },
                    { 3, null, "Refund Player" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "TransactionAuditTrail");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
