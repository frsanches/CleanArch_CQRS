using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("b8be3eeb-ed31-4a1e-b139-44bd2a8920b4"), "john.doe@banking.com", "John", "Doe" });

            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "BankAccountId", "Balance", "CustomerId" },
                values: new object[] { new Guid("7aaa7cea-526a-4a4e-8a0b-daa180438421"), 1000.0, new Guid("b8be3eeb-ed31-4a1e-b139-44bd2a8920b4") });

            migrationBuilder.InsertData(
                table: "CreditTransaction",
                columns: new[] { "CreditTransactionId", "Amount", "BankAccountId", "Description" },
                values: new object[] { new Guid("fd842dfd-9fc3-4c72-b810-a1407c0b79ea"), 1000.0, new Guid("7aaa7cea-526a-4a4e-8a0b-daa180438421"), "Credit Transaction" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditTransaction",
                keyColumn: "CreditTransactionId",
                keyValue: new Guid("fd842dfd-9fc3-4c72-b810-a1407c0b79ea"));

            migrationBuilder.DeleteData(
                table: "BankAccount",
                keyColumn: "BankAccountId",
                keyValue: new Guid("7aaa7cea-526a-4a4e-8a0b-daa180438421"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: new Guid("b8be3eeb-ed31-4a1e-b139-44bd2a8920b4"));
        }
    }
}
