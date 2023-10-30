using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReDoProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4_my_mistake_sorry_guys_i_was_hoping_customer_is_person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Instruments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Brands",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Baskets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_CustomerId",
                table: "Instruments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CustomerId",
                table: "Brands",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Customers_CustomerId",
                table: "Brands",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_Customers_CustomerId",
                table: "Instruments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Customers_CustomerId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Customers_CustomerId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_Customers_CustomerId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_CustomerId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CustomerId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Baskets");
        }
    }
}
