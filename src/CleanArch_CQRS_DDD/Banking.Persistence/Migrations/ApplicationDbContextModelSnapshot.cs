﻿// <auto-generated />
using System;
using Banking.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Banking.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Banking.Persistence.Entities.BankAccountTable", b =>
                {
                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("bank_account_id");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision")
                        .HasColumnName("balance");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.HasKey("BankAccountId")
                        .HasName("pk_bank_account");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_bank_account_customer_id");

                    b.ToTable("BankAccount", (string)null);

                    b.HasData(
                        new
                        {
                            BankAccountId = new Guid("5363399b-6d9f-4552-9af5-d49df4d4ff23"),
                            Balance = 1000.0,
                            CustomerId = new Guid("ad6ba7eb-253c-42de-bc1d-313aebdb519c")
                        });
                });

            modelBuilder.Entity("Banking.Persistence.Entities.CreditTransactionTable", b =>
                {
                    b.Property<Guid>("CreditTransactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("credit_transaction_id");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision")
                        .HasColumnName("amount");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("bank_account_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.HasKey("CreditTransactionId")
                        .HasName("pk_credit_transaction");

                    b.HasIndex("BankAccountId")
                        .HasDatabaseName("ix_credit_transaction_bank_account_id");

                    b.ToTable("CreditTransaction", (string)null);

                    b.HasData(
                        new
                        {
                            CreditTransactionId = new Guid("0fd778c9-9200-4167-b6d8-8b9f21880aac"),
                            Amount = 1000.0,
                            BankAccountId = new Guid("5363399b-6d9f-4552-9af5-d49df4d4ff23"),
                            Description = "Credit Transaction"
                        });
                });

            modelBuilder.Entity("Banking.Persistence.Entities.CustomerTable", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("SSN")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("ssn");

                    b.HasKey("CustomerId")
                        .HasName("pk_customer");

                    b.ToTable("Customer", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = new Guid("ad6ba7eb-253c-42de-bc1d-313aebdb519c"),
                            Email = "john.doe@banking.com",
                            FirstName = "John",
                            LastName = "Doe",
                            SSN = "545-40-9491"
                        });
                });

            modelBuilder.Entity("Banking.Persistence.Entities.DebitTransactionTable", b =>
                {
                    b.Property<Guid>("DebitTransactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("debit_transaction_id");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision")
                        .HasColumnName("amount");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("bank_account_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.HasKey("DebitTransactionId")
                        .HasName("pk_debit_transaction");

                    b.HasIndex("BankAccountId")
                        .HasDatabaseName("ix_debit_transaction_bank_account_id");

                    b.ToTable("DebitTransaction", (string)null);
                });

            modelBuilder.Entity("Banking.Persistence.Entities.BankAccountTable", b =>
                {
                    b.HasOne("Banking.Persistence.Entities.CustomerTable", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bank_account_customer_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Banking.Persistence.Entities.CreditTransactionTable", b =>
                {
                    b.HasOne("Banking.Persistence.Entities.BankAccountTable", "BankAccount")
                        .WithMany("CreditTransactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_credit_transaction_bank_account_bank_account_id");

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("Banking.Persistence.Entities.DebitTransactionTable", b =>
                {
                    b.HasOne("Banking.Persistence.Entities.BankAccountTable", "BankAccount")
                        .WithMany("DebitTransactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_debit_transaction_bank_account_bank_account_id");

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("Banking.Persistence.Entities.BankAccountTable", b =>
                {
                    b.Navigation("CreditTransactions");

                    b.Navigation("DebitTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
