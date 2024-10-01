using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ssn = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_account", x => x.bank_account_id);
                    table.ForeignKey(
                        name: "fk_bank_account_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditTransaction",
                columns: table => new
                {
                    credit_transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credit_transaction", x => x.credit_transaction_id);
                    table.ForeignKey(
                        name: "fk_credit_transaction_bank_account_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "BankAccount",
                        principalColumn: "bank_account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebitTransaction",
                columns: table => new
                {
                    debit_transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_debit_transaction", x => x.debit_transaction_id);
                    table.ForeignKey(
                        name: "fk_debit_transaction_bank_account_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "BankAccount",
                        principalColumn: "bank_account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "customer_id", "email", "first_name", "last_name", "ssn" },
                values: new object[] { new Guid("ad6ba7eb-253c-42de-bc1d-313aebdb519c"), "john.doe@banking.com", "John", "Doe", "545-40-9491" });

            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "bank_account_id", "balance", "customer_id" },
                values: new object[] { new Guid("5363399b-6d9f-4552-9af5-d49df4d4ff23"), 1000.0, new Guid("ad6ba7eb-253c-42de-bc1d-313aebdb519c") });

            migrationBuilder.InsertData(
                table: "CreditTransaction",
                columns: new[] { "credit_transaction_id", "amount", "bank_account_id", "description" },
                values: new object[] { new Guid("0fd778c9-9200-4167-b6d8-8b9f21880aac"), 1000.0, new Guid("5363399b-6d9f-4552-9af5-d49df4d4ff23"), "Credit Transaction" });

            migrationBuilder.CreateIndex(
                name: "ix_bank_account_customer_id",
                table: "BankAccount",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_credit_transaction_bank_account_id",
                table: "CreditTransaction",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_debit_transaction_bank_account_id",
                table: "DebitTransaction",
                column: "bank_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditTransaction");

            migrationBuilder.DropTable(
                name: "DebitTransaction");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
