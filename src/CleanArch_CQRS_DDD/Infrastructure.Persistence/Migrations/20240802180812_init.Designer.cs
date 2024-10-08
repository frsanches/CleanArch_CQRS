﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240802180812_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Infrastructure.Persistence.Entities.BankAccountTable", b =>
                {
                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.HasKey("BankAccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.CreditTransactionTable", b =>
                {
                    b.Property<Guid>("CreditTransactionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("CreditTransactionId");

                    b.HasIndex("BankAccountId");

                    b.ToTable("CreditTransaction", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.CustomerTable", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.DebitTransactionTable", b =>
                {
                    b.Property<Guid>("DebitTransactionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("DebitTransactionId");

                    b.HasIndex("BankAccountId");

                    b.ToTable("DebitTransaction", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.BankAccountTable", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.CustomerTable", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.CreditTransactionTable", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.BankAccountTable", "BankAccount")
                        .WithMany("CreditTransactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.DebitTransactionTable", b =>
                {
                    b.HasOne("Infrastructure.Persistence.Entities.BankAccountTable", "BankAccount")
                        .WithMany("DebitTransactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("Infrastructure.Persistence.Entities.BankAccountTable", b =>
                {
                    b.Navigation("CreditTransactions");

                    b.Navigation("DebitTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
