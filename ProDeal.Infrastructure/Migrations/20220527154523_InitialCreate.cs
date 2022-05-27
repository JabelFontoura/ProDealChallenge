using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProDeal.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<int>(type: "integer", nullable: false),
                    parent_external_id = table.Column<int>(type: "integer", nullable: true),
                    item_name = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_id", x => x.id);
                    table.UniqueConstraint("AK_FolderItems_external_id", x => x.external_id);
                    table.ForeignKey(
                        name: "FK_FolderItems_FolderItems_parent_external_id",
                        column: x => x.parent_external_id,
                        principalTable: "FolderItems",
                        principalColumn: "external_id");
                });

            migrationBuilder.CreateIndex(
                name: "Index_external_id",
                table: "FolderItems",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FolderItems_parent_external_id",
                table: "FolderItems",
                column: "parent_external_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderItems");
        }
    }
}
