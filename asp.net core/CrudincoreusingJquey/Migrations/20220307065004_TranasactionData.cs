﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudincoreusingJquey.Migrations
{
    public partial class TranasactionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tranascations",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SWIFTCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tranascations", x => x.TransactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tranascations");
        }
    }
}
