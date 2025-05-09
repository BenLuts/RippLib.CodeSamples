using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsideInTestingDemo.App.PostgreSql.Migrations;

/// <inheritdoc />
public partial class RemoveNullableRefTypes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Summary",
            table: "WeatherForecast",
            type: "character varying(50)",
            maxLength: 50,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(50)",
            oldMaxLength: 50);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Summary",
            table: "WeatherForecast",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(50)",
            oldMaxLength: 50,
            oldNullable: true);
    }
}
