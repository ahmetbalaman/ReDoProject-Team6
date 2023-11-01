using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReDoProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Basket_OrderedBasketId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Basket_BasketId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedInstruments_Basket_BasketId",
                table: "OrderedInstruments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedInstruments_Instruments_InstrumentId",
                table: "OrderedInstruments");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OrderedBasketId",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedInstruments",
                table: "OrderedInstruments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "OrderedBasketId",
                table: "Baskets");

            migrationBuilder.RenameTable(
                name: "OrderedInstruments",
                newName: "OrderedInstrument");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedInstruments_InstrumentId",
                table: "OrderedInstrument",
                newName: "IX_OrderedInstrument_InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedInstruments_BasketId",
                table: "OrderedInstrument",
                newName: "IX_OrderedInstrument_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedInstrument",
                table: "OrderedInstrument",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderedBasketId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedByUserId = table.Column<string>(type: "text", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Baskets_OrderedBasketId",
                        column: x => x.OrderedBasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedBasketId",
                table: "Orders",
                column: "OrderedBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Baskets_BasketId",
                table: "Customers",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedInstrument_Baskets_BasketId",
                table: "OrderedInstrument",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedInstrument_Instruments_InstrumentId",
                table: "OrderedInstrument",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Baskets_BasketId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedInstrument_Baskets_BasketId",
                table: "OrderedInstrument");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedInstrument_Instruments_InstrumentId",
                table: "OrderedInstrument");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderedInstrument",
                table: "OrderedInstrument");

            migrationBuilder.RenameTable(
                name: "OrderedInstrument",
                newName: "OrderedInstruments");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedInstrument_InstrumentId",
                table: "OrderedInstruments",
                newName: "IX_OrderedInstruments_InstrumentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedInstrument_BasketId",
                table: "OrderedInstruments",
                newName: "IX_OrderedInstruments_BasketId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Baskets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Baskets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderedBasketId",
                table: "Baskets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderedInstruments",
                table: "OrderedInstruments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedByUserId = table.Column<string>(type: "text", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedByUserId = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OrderedBasketId",
                table: "Baskets",
                column: "OrderedBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Basket_OrderedBasketId",
                table: "Baskets",
                column: "OrderedBasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Basket_BasketId",
                table: "Customers",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedInstruments_Basket_BasketId",
                table: "OrderedInstruments",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedInstruments_Instruments_InstrumentId",
                table: "OrderedInstruments",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
