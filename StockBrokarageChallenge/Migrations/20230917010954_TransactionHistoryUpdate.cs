﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockBrokarageChallenge.Application.Migrations
{
    /// <inheritdoc />
    public partial class TransactionHistoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StockPrice",
                table: "TransactionsHistory",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockPrice",
                table: "TransactionsHistory");
        }
    }
}
