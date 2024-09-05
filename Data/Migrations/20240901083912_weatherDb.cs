using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class weatherDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    Tz_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localtime_Epoch = table.Column<long>(type: "bigint", nullable: false),
                    Localtime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Last_Updated_Epoch = table.Column<long>(type: "bigint", nullable: false),
                    Last_Updated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temp_C = table.Column<double>(type: "float", nullable: false),
                    Temp_F = table.Column<double>(type: "float", nullable: false),
                    Is_Day = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    Wind_Mph = table.Column<double>(type: "float", nullable: false),
                    Wind_Kph = table.Column<double>(type: "float", nullable: false),
                    Wind_Degree = table.Column<int>(type: "int", nullable: false),
                    Wind_Dir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pressure_Mb = table.Column<double>(type: "float", nullable: false),
                    Pressure_In = table.Column<double>(type: "float", nullable: false),
                    Precip_Mm = table.Column<double>(type: "float", nullable: false),
                    Precip_In = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Cloud = table.Column<int>(type: "int", nullable: false),
                    Feelslike_C = table.Column<double>(type: "float", nullable: false),
                    Feelslike_F = table.Column<double>(type: "float", nullable: false),
                    Windchill_C = table.Column<double>(type: "float", nullable: false),
                    Windchill_F = table.Column<double>(type: "float", nullable: false),
                    Heatindex_C = table.Column<double>(type: "float", nullable: false),
                    Heatindex_F = table.Column<double>(type: "float", nullable: false),
                    Dewpoint_C = table.Column<double>(type: "float", nullable: false),
                    Dewpoint_F = table.Column<double>(type: "float", nullable: false),
                    Vis_Km = table.Column<double>(type: "float", nullable: false),
                    Vis_Miles = table.Column<double>(type: "float", nullable: false),
                    Uv = table.Column<double>(type: "float", nullable: false),
                    Gust_Mph = table.Column<double>(type: "float", nullable: false),
                    Gust_Kph = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currents_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currents_ConditionId",
                table: "Currents",
                column: "ConditionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Conditions");
        }
    }
}
