using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Doctor.Migrations
{
    /// <inheritdoc />
    public partial class DoctorUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                schema: "ProjectSzT3",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                schema: "ProjectSzT3",
                table: "Doctor");
        }
    }
}
