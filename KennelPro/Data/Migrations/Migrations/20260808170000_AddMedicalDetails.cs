using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KennelPro.Migrations;

public partial class AddMedicalDetails : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(name: "Symptoms", table: "Diseases", type: "TEXT", nullable: true);
        migrationBuilder.AddColumn<DateTime>(name: "StartDate", table: "Diseases", type: "TEXT", nullable: false,
            defaultValue: new DateTime(2026, 8, 8));
        migrationBuilder.AddColumn<DateTime>(name: "RecoveryDate", table: "Diseases", type: "TEXT", nullable: true);
        migrationBuilder.AddColumn<string>(name: "Status", table: "Diseases", type: "TEXT", nullable: false,
            defaultValue: "Active");

        migrationBuilder.AddColumn<string>(name: "Dosage", table: "Medications", type: "TEXT", nullable: true);
        migrationBuilder.AddColumn<string>(name: "Frequency", table: "Medications", type: "TEXT", nullable: true);
        migrationBuilder.AddColumn<DateTime>(name: "StartDate", table: "Medications", type: "TEXT", nullable: false,
            defaultValue: new DateTime(2026, 8, 8));
        migrationBuilder.AddColumn<DateTime>(name: "EndDate", table: "Medications", type: "TEXT", nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Symptoms", table: "Diseases");
        migrationBuilder.DropColumn(name: "StartDate", table: "Diseases");
        migrationBuilder.DropColumn(name: "RecoveryDate", table: "Diseases");
        migrationBuilder.DropColumn(name: "Status", table: "Diseases");
        migrationBuilder.DropColumn(name: "Dosage", table: "Medications");
        migrationBuilder.DropColumn(name: "Frequency", table: "Medications");
        migrationBuilder.DropColumn(name: "StartDate", table: "Medications");
        migrationBuilder.DropColumn(name: "EndDate", table: "Medications");
    }
}
