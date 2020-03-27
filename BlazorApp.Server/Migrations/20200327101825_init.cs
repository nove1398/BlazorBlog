using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorApp.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    PostThreshold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Username = table.Column<string>(maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Birthday = table.Column<DateTime>(maxLength: 50, nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 250, nullable: false),
                    Salt = table.Column<string>(maxLength: 250, nullable: false),
                    Avatar = table.Column<string>(maxLength: 250, nullable: true),
                    LevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ReadAt = table.Column<DateTime>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false, defaultValue: false),
                    UserFromId = table.Column<int>(nullable: false),
                    UserToId = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserToId",
                        column: x => x.UserToId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRolesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRolesId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "LevelId", "Description", "Name", "PostThreshold" },
                values: new object[,]
                {
                    { 1, "a new blogger on the platform, a littl bit shy. Warm hugs are needed.", "baby blogger", 0 },
                    { 2, "a more seasoned blogger on the platform, a little bit shy.", "junior blogger", 0 },
                    { 3, "a blogger that has been around for a bit, usually able to show you the ropes.", "expert blogger", 0 },
                    { 4, "probably been around since the starting of time! Usually knows everything about everything. Definitely able to help the new ones!", "adept blogger", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Avatar", "Birthday", "Email", "FirstName", "LastName", "LevelId", "Password", "Salt", "Status", "Username" },
                values: new object[] { 2, "", new DateTime(1989, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "drew2@yahoo.com", "2", "2", 1, "password", "salty", "Pending", "TempName2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Avatar", "Birthday", "Email", "FirstName", "LastName", "LevelId", "Password", "Salt", "Status", "Username" },
                values: new object[] { 1, "", new DateTime(1989, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "drew@yahoo.com", "nove", "drew", 4, "password", "salty", "Active", "TempName" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserFromId",
                table: "Messages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserToId",
                table: "Messages",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LevelId",
                table: "Users",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Level");
        }
    }
}
