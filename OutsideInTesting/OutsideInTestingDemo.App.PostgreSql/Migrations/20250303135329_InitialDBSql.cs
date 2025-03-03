using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsideInTestingDemo.App.PostgreSql.Migrations;

/// <inheritdoc />
public partial class InitialDBSql : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WeatherForecast",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Date = table.Column<DateOnly>(type: "date", nullable: false),
                TemperatureC = table.Column<int>(type: "integer", nullable: false),
                Summary = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WeatherForecast", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "WeatherForecast");
    }
}
