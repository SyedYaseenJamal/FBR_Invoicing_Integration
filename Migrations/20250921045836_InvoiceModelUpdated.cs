using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBR_Invoicing_Integration.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerAddress",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerRegistrationNo",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerType",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationOfSupply",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceType",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaleOriginProvince",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SaleType",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraTax",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedRetailPrice",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FurtherTax",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ItemHsCode",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemSrNo",
                table: "InvoiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "STWithheldAtSource",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SroScheduleNo",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UOM",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ValueOfSalesExclST",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerAddress",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerRegistrationNo",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BuyerType",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DestinationOfSupply",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SaleOriginProvince",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SaleType",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ExtraTax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "FixedRetailPrice",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "FurtherTax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ItemHsCode",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ItemSrNo",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "STWithheldAtSource",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "SroScheduleNo",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UOM",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ValueOfSalesExclST",
                table: "InvoiceItems");
        }
    }
}
