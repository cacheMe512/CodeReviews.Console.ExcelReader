using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelReader.cacheMe512.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    SalesOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<byte>(type: "tinyint", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    OnlineOrderFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    SalesPersonID = table.Column<int>(type: "int", nullable: true),
                    TerritoryID = table.Column<int>(type: "int", nullable: true),
                    BillToAddressID = table.Column<int>(type: "int", nullable: true),
                    ShipToAddressID = table.Column<int>(type: "int", nullable: true),
                    ShipMethodID = table.Column<int>(type: "int", nullable: true),
                    CreditCardID = table.Column<int>(type: "int", nullable: true),
                    CreditCardApprovalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyRateID = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Freight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.SalesOrderID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrders");
        }
    }
}
