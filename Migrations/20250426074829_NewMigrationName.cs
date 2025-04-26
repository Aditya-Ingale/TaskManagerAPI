using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    public partial class NewMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter the 'Status' column to 'integer' type
            // Use 'USING' to convert the existing string values to integer
            migrationBuilder.Sql(
                "ALTER TABLE \"TaskItems\" ALTER COLUMN \"Status\" TYPE integer USING \"Status\"::integer;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the 'Status' column back to 'string' type
            migrationBuilder.Sql(
                "ALTER TABLE \"TaskItems\" ALTER COLUMN \"Status\" TYPE text USING \"Status\"::text;"
            );
        }
    }
}
