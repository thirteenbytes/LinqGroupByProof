using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseFix.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GovermentIdPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovermentIdPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ActiveMemberPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_MemberPhotos_ActiveMemberPhotoId",
                        column: x => x.ActiveMemberPhotoId,
                        principalTable: "MemberPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_ActiveMemberPhotoId",
                table: "Members",
                column: "ActiveMemberPhotoId",
                unique: true,
                filter: "[ActiveMemberPhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MemberId",
                table: "Photos",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_GovermentIdPhotos_Photos_Id",
                table: "GovermentIdPhotos",
                column: "Id",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPhotos_Photos_Id",
                table: "MemberPhotos",
                column: "Id",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPhotos_Photos_Id",
                table: "MemberPhotos");

            migrationBuilder.DropTable(
                name: "GovermentIdPhotos");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MemberPhotos");
        }
    }
}
