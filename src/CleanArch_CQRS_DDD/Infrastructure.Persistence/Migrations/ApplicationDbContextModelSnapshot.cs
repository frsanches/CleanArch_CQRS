﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasData(
                        new
                        {
                            BankAccountId = new Guid("7aaa7cea-526a-4a4e-8a0b-daa180438421"),
                            Balance = 1000.0,
                            CustomerId = new Guid("b8be3eeb-ed31-4a1e-b139-44bd2a8920b4")
                        });
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

                    b.HasData(
                        new
                        {
                            CreditTransactionId = new Guid("fd842dfd-9fc3-4c72-b810-a1407c0b79ea"),
                            Amount = 1000.0,
                            BankAccountId = new Guid("7aaa7cea-526a-4a4e-8a0b-daa180438421"),
                            Description = "Credit Transaction"
                        });
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

                    b.HasData(
                        new
                        {
                            CustomerId = new Guid("b8be3eeb-ed31-4a1e-b139-44bd2a8920b4"),
                            Email = "john.doe@banking.com",
                            FirstName = "John",
                            LastName = "Doe"
                        });
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
