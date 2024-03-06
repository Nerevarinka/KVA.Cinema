using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KVA.Cinema.Migrations
{
    public partial class UpdateVideoTagRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectsTags");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TagVideo",
                columns: table => new
                {
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagVideo", x => new { x.TagsId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_TagVideo_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagVideo_Videos_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SubscriptionId",
                table: "Tags",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TagVideo_VideosId",
                table: "TagVideo",
                column: "VideosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Subscriptions_SubscriptionId",
                table: "Tags",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Subscriptions_SubscriptionId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "TagVideo");

            migrationBuilder.DropIndex(
                name: "IX_Tags_SubscriptionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "ObjectsTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectsTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectsTags_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObjectsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectsTags_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObjectsTags_SubscriptionId",
                table: "ObjectsTags",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectsTags_TagId",
                table: "ObjectsTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectsTags_VideoId",
                table: "ObjectsTags",
                column: "VideoId");
        }
    }
}
