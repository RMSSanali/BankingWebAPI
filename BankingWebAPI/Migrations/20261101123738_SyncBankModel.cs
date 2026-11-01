using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SyncBankModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountType_AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Dispositions_DispositionId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispositions_Accounts_AccountId",
                table: "Dispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispositions_Customers_CustomerId",
                table: "Dispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountType",
                table: "AccountType");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transactions",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Loans",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Dispositions",
                newName: "Dispositions",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Cards",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Accounts",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AccountType",
                newName: "AccountTypes",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "BalanceAfterTransaction",
                schema: "dbo",
                table: "Transactions",
                newName: "Balance");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "Loans",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "AccountTypeId",
                schema: "dbo",
                table: "Accounts",
                newName: "AccountTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountTypeId",
                schema: "dbo",
                table: "Accounts",
                newName: "IX_Accounts_AccountTypesId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operation",
                schema: "dbo",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "dbo",
                table: "Loans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Payments",
                schema: "dbo",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CVV2",
                schema: "dbo",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AccountTypesId",
                schema: "dbo",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "dbo",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                schema: "dbo",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountTypes",
                schema: "dbo",
                table: "AccountTypes",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                schema: "dbo",
                table: "Loans",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes",
                schema: "dbo",
                table: "Accounts",
                column: "AccountTypesId",
                principalSchema: "dbo",
                principalTable: "AccountTypes",
                principalColumn: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Dispositions",
                schema: "dbo",
                table: "Cards",
                column: "DispositionId",
                principalSchema: "dbo",
                principalTable: "Dispositions",
                principalColumn: "DispositionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositions_Accounts",
                schema: "dbo",
                table: "Dispositions",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositions_Customers",
                schema: "dbo",
                table: "Dispositions",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Accounts",
                schema: "dbo",
                table: "Loans",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts",
                schema: "dbo",
                table: "Transactions",
                column: "AccountId",
                principalSchema: "dbo",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes",
                schema: "dbo",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Dispositions",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispositions_Accounts",
                schema: "dbo",
                table: "Dispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispositions_Customers",
                schema: "dbo",
                table: "Dispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Accounts",
                schema: "dbo",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts",
                schema: "dbo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                schema: "dbo",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountTypes",
                schema: "dbo",
                table: "AccountTypes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Operation",
                schema: "dbo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "dbo",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Payments",
                schema: "dbo",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CVV2",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "dbo",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Frequency",
                schema: "dbo",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "dbo",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Loans",
                schema: "dbo",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "Dispositions",
                schema: "dbo",
                newName: "Dispositions");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "dbo",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Cards",
                schema: "dbo",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "dbo",
                newName: "Accounts");

            migrationBuilder.RenameTable(
                name: "AccountTypes",
                schema: "dbo",
                newName: "AccountType");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Transactions",
                newName: "BalanceAfterTransaction");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Loans",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "AccountTypesId",
                table: "Accounts",
                newName: "AccountTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountTypesId",
                table: "Accounts",
                newName: "IX_Accounts_AccountTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountTypeId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountType",
                table: "AccountType",
                column: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountType_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId",
                principalTable: "AccountType",
                principalColumn: "AccountTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Dispositions_DispositionId",
                table: "Cards",
                column: "DispositionId",
                principalTable: "Dispositions",
                principalColumn: "DispositionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositions_Accounts_AccountId",
                table: "Dispositions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositions_Customers_CustomerId",
                table: "Dispositions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
