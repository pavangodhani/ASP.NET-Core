using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetInfo.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetSeriesNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetName", "AssetSeriesNo", "MachineType" },
                values: new object[,]
                {
                    { 1, "Cutter head", "S6", "C300" },
                    { 2, "Cutter head", "S7", "C40" },
                    { 3, "Blade safety cover", "S10", "C300" },
                    { 4, "Blade safety cover", "S11", "C60" },
                    { 5, "Deburring blades", "S6", "C300" },
                    { 6, "Cutter head", "S8", "C60" },
                    { 7, "Clamping fixture", "S2", "C60" },
                    { 8, "Blade safety cover", "S11", "C40" },
                    { 9, "Shutter gripper", "S3", "C40" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
