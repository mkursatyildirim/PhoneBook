using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.API.Migrations
{
    public partial class DBv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                table: "ContactInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "persons");

            migrationBuilder.RenameTable(
                name: "ContactInformations",
                newName: "contact_informations");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformations_PersonUUID",
                table: "contact_informations",
                newName: "IX_contact_informations_PersonUUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persons",
                table: "persons",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_informations",
                table: "contact_informations",
                column: "UUID");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_informations_persons_PersonUUID",
                table: "contact_informations",
                column: "PersonUUID",
                principalTable: "persons",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_informations_persons_PersonUUID",
                table: "contact_informations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persons",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_informations",
                table: "contact_informations");

            migrationBuilder.RenameTable(
                name: "persons",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "contact_informations",
                newName: "ContactInformations");

            migrationBuilder.RenameIndex(
                name: "IX_contact_informations_PersonUUID",
                table: "ContactInformations",
                newName: "IX_ContactInformations_PersonUUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "UUID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations",
                column: "UUID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                table: "ContactInformations",
                column: "PersonUUID",
                principalTable: "Persons",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
