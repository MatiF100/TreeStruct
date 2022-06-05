using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TreeStruct.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreeNode",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "text", nullable: false),
                    TreeNodeID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeNode", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TreeNode_TreeNode_TreeNodeID",
                        column: x => x.TreeNodeID,
                        principalTable: "TreeNode",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreeNode_TreeNodeID",
                table: "TreeNode",
                column: "TreeNodeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeNode");
        }
    }
}
