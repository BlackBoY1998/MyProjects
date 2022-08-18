using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestCodeFirstEmail.Migrations
{
    public partial class AddEmailInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    server = table.Column<string>(type: "text", nullable: false),
                    PortNo = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Passphrase = table.Column<string>(type: "text", nullable: true),
                    RequireAuthentication = table.Column<int>(type: "integer", nullable: false),
                    UseSSL = table.Column<int>(type: "integer", nullable: false),
                    FromAddress = table.Column<string>(type: "text", nullable: true),
                    FromDisplayName = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}
