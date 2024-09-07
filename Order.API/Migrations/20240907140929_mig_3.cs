using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orderoutboxes",
                table: "Orderoutboxes");

            migrationBuilder.RenameTable(
                name: "Orderoutboxes",
                newName: "OrderOutboxes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOutboxes",
                table: "OrderOutboxes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOutboxes",
                table: "OrderOutboxes");

            migrationBuilder.RenameTable(
                name: "OrderOutboxes",
                newName: "Orderoutboxes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orderoutboxes",
                table: "Orderoutboxes",
                column: "Id");
        }
    }
}
