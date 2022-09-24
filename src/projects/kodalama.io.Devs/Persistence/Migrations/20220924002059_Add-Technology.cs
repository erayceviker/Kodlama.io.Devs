using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddTechnology : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "Name", "ProgrammingLanguageId" },
                values: new object[,]
                {
                    { 1, "Vue", 1 },
                    { 2, "React", 1 },
                    { 3, "WPF", 2 },
                    { 4, "ASP.NET", 2 },
                    { 5, "Spring", 3 },
                    { 6, "JSP", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_ProgrammingLanguageId",
                table: "Technologies",
                column: "ProgrammingLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Technologies");
        }
    }
}
