using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelectahClinic.Migrations
{
    /// <inheritdoc />
    public partial class NovoCampoConvenio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Convenio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Convenio",
                table: "AspNetUsers");
        }
    }
}
