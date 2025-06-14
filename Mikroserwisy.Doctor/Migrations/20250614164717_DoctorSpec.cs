using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Doctor.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialization",
                schema: "ProjectSzT3",
                table: "Doctor",
                newName: "DoctorSpecialization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorSpecialization",
                schema: "ProjectSzT3",
                table: "Doctor",
                newName: "Specialization");
        }
    }
}
