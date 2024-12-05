using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinqGroupByProof.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DateTaken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberPhotos_Photos_Id",
                        column: x => x.Id,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_MemberPhotos_MemberId",
                table: "MemberPhotos",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ActiveMemberPhotoId",
                table: "Members",
                column: "ActiveMemberPhotoId",
                unique: true,
                filter: "[ActiveMemberPhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPhotos_Members_MemberId",
                table: "MemberPhotos",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPhotos_Members_MemberId",
                table: "MemberPhotos");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MemberPhotos");

            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
