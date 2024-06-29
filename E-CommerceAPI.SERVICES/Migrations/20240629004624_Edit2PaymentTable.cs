using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceAPI.SERVICES.Migrations
{
    /// <inheritdoc />
    public partial class Edit2PaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Payments");
        }
    }
}
