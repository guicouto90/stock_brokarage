﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockBrokarageChallenge.Application.Shared.Data.Context;

#nullable disable

namespace StockBrokarageChallenge.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230917010954_TransactionHistoryUpdate")]
    partial class TransactionHistoryUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.StockHistoryPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ActualPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("float(10)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("StocksHistoryPrices");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.StocksWallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AveragePrice")
                        .HasColumnType("float");

                    b.Property<double>("CurrentInvestedStock")
                        .HasColumnType("float");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalInvestedStock")
                        .HasColumnType("float");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("WalletId");

                    b.ToTable("StocksWallets");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockCode")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<double?>("StockPrice")
                        .HasColumnType("float");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<double>("TransactionValue")
                        .HasColumnType("float");

                    b.Property<string>("TypeTransaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TransactionsHistory");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<double>("CurrentBalance")
                        .HasColumnType("float");

                    b.Property<double>("TotalInvested")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Account", b =>
                {
                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Customer", "Customer")
                        .WithOne("Account")
                        .HasForeignKey("StockBrokarageChallenge.Application.Shared.Models.Account", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.StockHistoryPrice", b =>
                {
                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Stock", null)
                        .WithMany("History")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.StocksWallet", b =>
                {
                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Wallet", null)
                        .WithMany("StocksWallet")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.TransactionHistory", b =>
                {
                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Account", null)
                        .WithMany("TransactionHistories")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Wallet", b =>
                {
                    b.HasOne("StockBrokarageChallenge.Application.Shared.Models.Account", null)
                        .WithOne("Wallet")
                        .HasForeignKey("StockBrokarageChallenge.Application.Shared.Models.Wallet", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Account", b =>
                {
                    b.Navigation("TransactionHistories");

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Customer", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Stock", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("StockBrokarageChallenge.Application.Shared.Models.Wallet", b =>
                {
                    b.Navigation("StocksWallet");
                });
#pragma warning restore 612, 618
        }
    }
}
